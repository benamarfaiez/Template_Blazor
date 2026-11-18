$ErrorActionPreference = 'Stop'

$context = @()

# 1. Vérifier le SDK .NET
$dotnetCommand = Get-Command dotnet -ErrorAction SilentlyContinue

if (-not $dotnetCommand) {
    $context += "ALERTE : le SDK .NET n'est pas disponible dans cet environnement."
    $context += "Ne lance pas de build ou de test tant que l'environnement n'est pas corrigé."
} else {
    $dotnetVersion = dotnet --version 2>$null
    $context += "SDK .NET détecté : $dotnetVersion."
}

# 2. Détecter la solution
$solutions = Get-ChildItem -Path . -Filter *.sln -Recurse -Depth 2 -ErrorAction SilentlyContinue

if ($solutions) {
    $context += "Solutions détectées :"
    $context += ($solutions.FullName -join "`n")
} else {
    $context += "ALERTE : aucun fichier .sln détecté à moins de deux niveaux de profondeur."
    $context += "Identifie les projets .csproj avant de lancer un build ou des tests."
}

# 3. Détecter les projets Blazor
$projectFiles = Get-ChildItem -Path . -Filter *.csproj -Recurse -ErrorAction SilentlyContinue |
    Where-Object { $_.FullName -notmatch '\\bin\\|\\obj\\' }

$blazorProjects = $projectFiles | Where-Object {
    (Get-Content $_.FullName -Raw) -match 'Microsoft\.AspNetCore\.Components|Microsoft\.AspNetCore\.Components\.WebAssembly|Microsoft\.AspNetCore\.Components\.WebView'
}

if ($blazorProjects) {
    $context += "Projets Blazor détectés :"
    $context += ($blazorProjects.FullName -join "`n")

    if (($blazorProjects.FullName -join "`n") -match 'WebAssembly') {
        $context += "Variante Blazor probable : WebAssembly."
    } elseif (($blazorProjects.FullName -join "`n") -match 'WebView') {
        $context += "Variante Blazor probable : Hybrid."
    } else {
        $context += "Variante Blazor probable : Server ou Auto. Confirme-la dans les fichiers projet."
    }
} else {
    $context += "ALERTE : aucun projet Blazor détecté. Confirme le type d'application avant de proposer des modifications."
}

# 4. Détecter les projets de tests
$testProjects = $projectFiles | Where-Object {
    (Get-Content $_.FullName -Raw) -match 'xunit|nunit|mstest|bunit|Microsoft\.Playwright'
}

if ($testProjects) {
    $context += "Projets de tests détectés :"
    $context += ($testProjects.FullName -join "`n")
} else {
    $context += "ALERTE : aucun projet de test détecté."
    $context += "Pour tout refactoring, propose d'abord des tests de caractérisation."
}

# 5. Détecter les migrations EF Core
$migrations = Get-ChildItem -Path . -Directory -Filter Migrations -Recurse -ErrorAction SilentlyContinue |
    Where-Object { $_.FullName -notmatch '\\bin\\|\\obj\\' }

if ($migrations) {
    $context += "Dossiers de migrations EF Core détectés :"
    $context += ($migrations.FullName -join "`n")
    $context += "Ne lance jamais une migration, une suppression de base ou une suppression de migration sans validation humaine explicite."
}

# 6. Détecter les fichiers sensibles
$sensitiveFiles = Get-ChildItem -Path . -Recurse -File -ErrorAction SilentlyContinue |
    Where-Object {
        $_.FullName -notmatch '\\bin\\|\\obj\\|\\.git\\' -and
        $_.Name -match '\.env$|\.pfx$|\.key$|secret'
    } |
    Select-Object -First 20

if ($sensitiveFiles) {
    $context += "Fichiers potentiellement sensibles détectés :"
    $context += ($sensitiveFiles.FullName -join "`n")
    $context += "Ne lis, ne copie et ne journalise jamais leur contenu."
}

# 7. Règles de sécurité Blazor
$context += @(
    "Règles de sécurité Blazor :",
    "- Applique l'autorisation côté serveur, même si l'UI masque une action.",
    "- Valide toute entrée utilisateur côté serveur.",
    "- Ne stocke jamais de secret dans le code, les tests ou les fichiers versionnés.",
    "- Utilise des requêtes paramétrées pour prévenir l'injection SQL.",
    "- Isole le JavaScript interop derrière un service dédié."
)

# 8. Règles de refactoring
$context += @(
    "Règles de refactoring :",
    "- Préserve le comportement utilisateur observable.",
    "- Ajoute ou confirme les tests de caractérisation avant modification.",
    "- Limite chaque PR à un périmètre réduit et réversible.",
    "- Exécute dotnet build --no-restore et dotnet test --no-build lorsque possible.",
    "- Ne mélange pas refactoring, nouvelle fonctionnalité, correction de bug et migration."
)

# 9. Commandes recommandées
$context += @(
    "Commandes recommandées :",
    "- dotnet restore",
    "- dotnet build --no-restore",
    "- dotnet test --no-build",
    '- dotnet test --collect:"XPlat Code Coverage"',
    "- dotnet format --verify-no-changes"
)

# 10. Format JSON pour additionalContext
$contextText = $context -join "`n"
$payload = @{
    additionalContext = $contextText
} | ConvertTo-Json -Depth 3 -Compress

Write-Output $payload