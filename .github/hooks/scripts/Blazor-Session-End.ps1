$ErrorActionPreference = 'Stop'

$input = [Console]::In.ReadToEnd()
$payload = $input | ConvertFrom-Json

$reason = $payload.reason
$finalMessage = $payload.finalMessage
$cwd = $payload.cwd

if ($cwd) {
    Set-Location $cwd
}

# 1. Journalisation locale, sans donnée sensible
$logDirectory = ".copilot-agent-logs"

if (-not (Test-Path $logDirectory)) {
    New-Item -ItemType Directory -Path $logDirectory | Out-Null
}

$logFile = Join-Path $logDirectory ("sessions-" + (Get-Date -Format "yyyy-MM-dd") + ".log")

$sanitized = $payload | ConvertTo-Json -Depth 10 -Compress
$sanitized = $sanitized -replace '(?i)(password|token|secret|apikey|api_key|connectionstring)=[^"&\s]+', '$1=[REDACTED]'

Add-Content -Path $logFile -Value $sanitized

# 2. Nettoyage prudent des artefacts temporaires connus
$cleanupPaths = @(
    ".copilot-test-artifacts",
    ".copilot-playwright-artifacts",
    "TestResults/temp"
)

foreach ($path in $cleanupPaths) {
    if (Test-Path $path) {
        Remove-Item -Path $path -Recurse -Force -ErrorAction SilentlyContinue
    }
}

# 3. Détecter les fichiers modifiés
$changedFiles = git diff --name-only --diff-filter=ACMR 2>$null
$stagedFiles = git diff --cached --name-only --diff-filter=ACMR 2>$null

$allChangedFiles = @($changedFiles + $stagedFiles) | Sort-Object -Unique

$context = @()

# 4. Résumé de fin de session
$context += @(
    "Session terminée.",
    "Raison de fin : $(if ($reason) { $reason } else { 'inconnue' })."
)

if ($changedFiles) {
    $context += @(
        "Fichiers modifiés non commités :",
        ($changedFiles -join "`n")
    )
}

if ($stagedFiles) {
    $context += @(
        "Fichiers indexés :",
        ($stagedFiles -join "`n")
    )
}

# 5. Vérification des fichiers sensibles
$sensitiveFiles = $allChangedFiles | Where-Object {
    $_ -match '\.env$|\.pfx$|\.key$|secret|credential|password|connectionstring'
}

if ($sensitiveFiles) {
    $context += @(
        "ALERTE : des fichiers potentiellement sensibles ont été modifiés :",
        ($sensitiveFiles -join "`n"),
        "Ne les committe pas et vérifie qu'ils ne contiennent aucun secret."
    )
}

# 6. Vérification des migrations
$migrationFiles = $allChangedFiles | Where-Object {
    $_ -match 'Migrations/|\.edmx'
}

if ($migrationFiles) {
    $context += @(
        "ALERTE : des migrations ou fichiers de base de données ont été modifiés :",
        ($migrationFiles -join "`n"),
        "Ne les applique pas automatiquement.",
        "Demande une validation humaine avant dotnet ef database update.",
        "Vérifie la réversibilité, les données existantes et le plan de rollback."
    )
}

# 7. Vérification des tests
$testFiles = $allChangedFiles | Where-Object {
    $_ -match 'tests?/|\.Tests\.|\.spec\.|\.test\.'
}

if ($testFiles) {
    $context += @(
        "Des tests ont été modifiés ou ajoutés :",
        ($testFiles -join "`n"),
        "Vérifie qu'ils sont déterministes, isolés et qu'ils valident le comportement observable."
    )
}

# 8. Vérification des composants Blazor
$blazorFiles = $allChangedFiles | Where-Object {
    $_ -match '\.razor$'
}

if ($blazorFiles) {
    $context += @(
        "Des composants Blazor ont été modifiés :",
        ($blazorFiles -join "`n"),
        "Vérifie la séparation UI / logique métier, les états de chargement, d'erreur et vide,",
        "l'accessibilité, la libération des ressources et les tests bUnit."
    )
}

# 9. Vérification de la CI/CD
$ciFiles = $allChangedFiles | Where-Object {
    $_ -match '\.github/workflows/|Dockerfile|docker-compose|kubernetes|terraform|helm'
}

if ($ciFiles) {
    $context += @(
        "ALERTE : des fichiers CI/CD ou d'infrastructure ont été modifiés :",
        ($ciFiles -join "`n"),
        "Ne déploie rien et ne fusionne rien sans validation humaine explicite.",
        "Vérifie les secrets, les permissions, les environnements et le rollback."
    )
}

# 10. Vérification de la dernière réponse
if ($finalMessage -match '(?i)secret|token|password|api key|connection string') {
    $context += @(
        "ALERTE : la dernière réponse peut contenir une donnée sensible.",
        "Ne la copie pas dans un fichier, un log, une PR ou un message utilisateur.",
        "Masque immédiatement toute valeur sensible."
    )
}

# 11. Recommandations finales
$context += @(
    "Avant de terminer :",
    "- Vérifie que le build et les tests pertinents ont été exécutés.",
    "- Vérifie qu'aucun secret n'est exposé.",
    "- Vérifie qu'aucune migration n'a été appliquée sans validation.",
    "- Vérifie qu'aucun fichier CI/CD ou d'infrastructure n'a été modifié sans validation.",
    "- Crée une PR claire si des modifications doivent être livrées."
)

# 12. Format JSON
$contextText = $context -join "`n"

$response = @{
    additionalContext = $contextText
}

Write-Output ($response | ConvertTo-Json -Depth 3 -Compress)