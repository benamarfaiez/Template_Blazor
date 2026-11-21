$ErrorActionPreference = 'Stop'

$input = [Console]::In.ReadToEnd()
$payload = $input | ConvertFrom-Json

$errorMessage = $payload.error
$errorContext = $payload.errorContext
$recoverable = $payload.recoverable
$toolName = $payload.toolName
$toolArgs = $payload.toolArgs | ConvertTo-Json -Depth 10 -Compress

# 1. Journalisation locale sans donnée sensible
$logDirectory = ".copilot-agent-logs"

if (-not (Test-Path $logDirectory)) {
    New-Item -ItemType Directory -Path $logDirectory | Out-Null
}

$logFile = Join-Path $logDirectory ("errors-" + (Get-Date -Format "yyyy-MM-dd") + ".log")

$sanitized = $payload | ConvertTo-Json -Depth 10 -Compress
$sanitized = $sanitized -replace '(?i)(password|token|secret|apikey|api_key|connectionstring)=[^"&\s]+', '$1=[REDACTED]'

Add-Content -Path $logFile -Value $sanitized

# 2. Conseils selon le type d’erreur
$context = @()

switch ($errorContext) {
    'tool_execution' {
        $context += @(
            "Une erreur est survenue pendant l'exécution d'un outil.",
            "Ne relance pas aveuglément la même commande.",
            "Analyse d'abord le message d'erreur, la commande exécutée et les fichiers concernés."
        )
    }
    'model_call' {
        $context += @(
            "Une erreur est survenue lors d'un appel au modèle.",
            "Ne répète pas automatiquement la requête.",
            "Simplifie la tâche, vérifie le contexte disponible ou demande une clarification."
        )
    }
    'system' {
        $context += @(
            "Une erreur système est survenue.",
            "Vérifie l'environnement, les permissions, l'espace disque et la disponibilité des outils requis.",
            "Ne tente pas de contourner une erreur système."
        )
    }
    'user_input' {
        $context += @(
            "L'entrée utilisateur est invalide ou ambiguë.",
            "Demande une clarification plutôt que de faire une hypothèse."
        )
    }
    default {
        $context += @(
            "Une erreur inconnue est survenue.",
            "Analyse le contexte et demande une clarification si la cause n'est pas évidente."
        )
    }
}

# 3. Erreurs .NET fréquentes
if ($errorMessage -match '(?i)NU\d{4}|restore failed|package.*not found|nuget') {
    $context += @(
        "Erreur liée à NuGet ou à la restauration des packages.",
        "- Vérifie la version .NET, les sources NuGet et la connectivité réseau.",
        "- Ne modifie pas une dépendance majeure sans validation humaine.",
        "- Si une dépendance est vulnérable, signale-la et propose une mise à jour contrôlée."
    )
}

if ($errorMessage -match '(?i)CS\d{4}|compilation error|build failed|MSB\d{4}') {
    $context += @(
        "Erreur de compilation .NET détectée.",
        "- Analyse les premières erreurs du compilateur.",
        "- Corrige la cause racine, pas seulement le symptôme.",
        "- Ne désactive pas une règle d'analyse, un avertissement ou un test pour masquer l'échec.",
        "- Après correction, exécute dotnet build --no-restore et les tests pertinents."
    )
}

if ($errorMessage -match '(?i)test failed|failed!|xunit|bunit|playwright|assert') {
    $context += @(
        "Échec de test détecté.",
        "- Classe l'échec : bug existant, régression ou problème de test.",
        "- Ne supprime ou ne désactive pas un test pour faire passer la suite.",
        "- Pour une régression, corrige le code de production.",
        "- Pour un problème de test, corrige le test, la fixture ou le mock.",
        "- Pour un bug existant, signale-le et attends une décision humaine."
    )
}

if ($errorMessage -match '(?i)ef core|migration|database|sql|connection refused|timeout') {
    $context += @(
        "Erreur potentielle liée à la base de données ou à EF Core.",
        "- Ne lance jamais une migration, une suppression de base ou une suppression de migration.",
        "- Vérifie la chaîne de connexion, les migrations en attente et les permissions.",
        "- Ne journalise pas de chaîne de connexion ou de secret.",
        "- Demande une validation humaine avant toute action destructive."
    )
}

if ($errorMessage -match '(?i)http|timeout|network|socket|certificate|tls|ssl') {
    $context += @(
        "Erreur réseau ou HTTP détectée.",
        "- Vérifie l'URL, le timeout, le certificat et la connectivité.",
        "- Ne désactive jamais la validation TLS.",
        "- Ne relance pas une requête en boucle sans politique de retry contrôlée.",
        "- Vérifie que CancellationToken est correctement utilisé."
    )
}

if ($errorMessage -match '(?i)permission denied|access denied|unauthorized|forbidden|401|403') {
    $context += @(
        "Erreur d'autorisation ou de permission détectée.",
        "- Ne contourne pas l'autorisation pour faire passer une action.",
        "- Vérifie les politiques d'autorisation, les rôles et les permissions côté serveur.",
        "- Signale le problème plutôt que de modifier les règles de sécurité."
    )
}

if ($errorMessage -match '(?i)secret|token|password|api key|connection string') {
    $context += @(
        "ALERTE : le message d'erreur peut contenir une donnée sensible.",
        "- Ne copie pas le secret dans un fichier, un log, une PR ou un message utilisateur.",
        "- Masque immédiatement la valeur sensible.",
        "- Vérifie que la configuration utilise un gestionnaire de secrets ou des variables d'environnement."
    )
}

# 4. Conseil de reprise
if ($recoverable -eq $true) {
    $context += @(
        "L'erreur peut potentiellement être récupérée.",
        "- Corrige d'abord la cause racine.",
        "- Relance uniquement l'action nécessaire, avec un périmètre réduit.",
        "- Vérifie le résultat avant de poursuivre."
    )
} else {
    $context += @(
        "L'erreur n'est pas considérée comme récupérable automatiquement.",
        "- Ne relance pas l'action.",
        "- Produis un diagnostic clair et demande une décision humaine."
    )
}

# 5. Contexte spécifique à l'outil
if ($toolName) {
    $context += @(
        "Outil concerné : $toolName.",
        "Arguments concernés : $toolArgs.",
        "Vérifie que les arguments ne contiennent pas de secret avant de les partager."
    )
}

# 6. Format JSON
$contextText = $context -join "`n"

$response = @{
    additionalContext = $contextText
}

Write-Output ($response | ConvertTo-Json -Depth 3 -Compress)