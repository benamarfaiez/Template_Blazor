# Hook PreToolUse — garde-fous déterministes pour agents Blazor.
# Retourne toujours un JSON {permissionDecision: allow|deny|ask}.
# Fail-closed : toute erreur de parsing = deny.

$ErrorActionPreference = 'Stop'

function EmitDecision {
    param([string]$Decision, [string]$Reason = '')
    $response = @{ permissionDecision = $Decision }
    if ($Reason) { $response['permissionDecisionReason'] = $Reason }
    Write-Output ($response | ConvertTo-Json -Depth 3 -Compress)
    exit 0
}

function Write-Log {
    param([string]$Message)
    [Console]::Error.WriteLine("[pre-tool-use] $Message")
}

# ---------------------------------------------------------------
# Lecture et validation de l'input
# ---------------------------------------------------------------
$inputRaw = [Console]::In.ReadToEnd()

if ($inputRaw.Length -gt 1048576) {
    EmitDecision -Decision 'deny' -Reason 'Hook PreToolUse : input trop volumineux (> 1 Mo).'
}

try {
    $payload = $inputRaw | ConvertFrom-Json -ErrorAction Stop
} catch {
    EmitDecision -Decision 'deny' -Reason 'Hook PreToolUse : input JSON invalide.'
}

$toolName = $payload.toolName
if (-not $toolName) {
    EmitDecision -Decision 'deny' -Reason 'Hook PreToolUse : toolName absent.'
}

# B14 : extraire récursivement toutes les chaînes de toolArgs
function Get-AllStrings {
    param($Node)
    if ($null -eq $Node) { return }
    if ($Node -is [string]) { Write-Output $Node; return }
    if ($Node -is [System.Collections.IEnumerable]) {
        foreach ($item in $Node) { Get-AllStrings -Node $item }
        return
    }
    if ($Node -is [System.Management.Automation.PSCustomObject]) {
        foreach ($prop in $Node.PSObject.Properties) {
            Get-AllStrings -Node $prop.Value
        }
        return
    }
    Write-Output ([string]$Node)
}

$toolArgStrings = @(
    Get-AllStrings -Node $payload.toolArgs |
        ForEach-Object { $_ -replace '\\', '/' }
)
$toolArgsStrings = $toolArgStrings -join "`n"
$toolCommand = $null
if ($payload.toolArgs -and $payload.toolArgs.PSObject.Properties['command'] -and $payload.toolArgs.command -is [string]) {
    $toolCommand = $payload.toolArgs.command
}

Write-Log "tool=$toolName"

# ---------------------------------------------------------------
# 1. Fichiers sensibles → ask
# ---------------------------------------------------------------
foreach ($toolArg in $toolArgStrings) {
    if ($toolArg -match '(?i)(^|/)(\.env(\..*)?$|secrets?\.(json|ya?ml)$|appsettings\.(Production|Staging)\.json$|.*\.(pfx|p12|key|pem)$|credentials?\.(json|ya?ml)$)') {
        EmitDecision -Decision 'ask' -Reason 'Fichier potentiellement sensible. Validation humaine requise.'
    }
}

# ---------------------------------------------------------------
# 2. Outils de lecture : allow
# ---------------------------------------------------------------
$readOnlyTools = @('view','read','search','glob','grep','list_files','get_file_contents','inspect')
if ($readOnlyTools -contains $toolName) {
    EmitDecision -Decision 'allow'
}

# ---------------------------------------------------------------
# 3. Outils shell
# ---------------------------------------------------------------
if ($toolName -in @('bash','shell','powershell')) {

    $blockedPatterns = @(
        'dotnet ef database drop'
        'dotnet ef migrations remove'
        'git push --force'
        'git push -f'
        'git push --force-with-lease'
        'git reset --hard'
        'git clean -fd'
        'git clean -fdx'
        'git clean -x'
        'git checkout -- .'
        'git branch -D'
        'rm -rf'
        'rm -fr'
        'rm -r -f'
        'rm --recursive --force'
        'rmdir /s /q'
        'del /s /q'
        'Remove-Item -Recurse -Force'
        'shutdown'
        'reboot'
        'mkfs'
        'dd if='
        'chmod 777'
        'kubectl delete'
        'docker system prune'
        'docker volume rm'
        'helm uninstall'
        'terraform destroy'
        'az group delete'
    )

    foreach ($pattern in $blockedPatterns) {
        if ($toolArgsStrings.IndexOf($pattern, [StringComparison]::OrdinalIgnoreCase) -ge 0) {
            EmitDecision -Decision 'deny' -Reason "Commande potentiellement destructive bloquée : $pattern."
        }
    }

    if ($toolArgsStrings -match '(?i)curl .*\|\s*(ba)?sh|wget .*\|\s*(ba)?sh|npm install|pnpm add|yarn add|dotnet tool install|dotnet add package') {
        EmitDecision -Decision 'ask' -Reason 'Installation de dépendance ou script externe. Validation humaine requise.'
    }

    $readOnlyReferencePattern = '\A[ \t]*git[ \t]+(branch|tag)([ \t]+--list)?[ \t]*\z'
    if ($toolCommand -match $readOnlyReferencePattern) {
        EmitDecision -Decision 'allow'
    }

    $allowedCommandPattern = '\A[ \t]*(dotnet[ \t]+(build|test|format|list[ \t]+package|restore|run|publish|watch|ef[ \t]+dbcontext[ \t]+script)|git[ \t]+(status|diff|log|show|remote|config[ \t]+--get|rev-parse|ls-files|blame|describe|reflog|stash[ \t]+list))([ \t]+[A-Za-z0-9_./:=,+@%-]+)*[ \t]*\z'
    if ($toolCommand -match $allowedCommandPattern) {
        if (-not ($toolCommand -match '(?i)git (branch\s+(-d|-m|-f|-c)|tag\s+(-d|-f)|remote\s+(add|remove|rename|set-url|prune)|stash\s+(drop|clear|pop)|push|reset|checkout|clean|commit|merge|rebase)')) {
            EmitDecision -Decision 'allow'
        }
    }

    EmitDecision -Decision 'ask' -Reason 'Commande shell non classée. Vérifie son impact avant exécution.'
}

# ---------------------------------------------------------------
# 4-7. Protections par chemin, uniquement sur opération modifiante
# ---------------------------------------------------------------
if ($toolName -in @('write','edit','create','delete','remove','move','rename')) {

    # 4. Migrations / DB
    foreach ($toolArg in $toolArgStrings) {
        if ($toolArg -match '(?i)(^|/)(Migrations?/|.*\.edmx$|appsettings\.json$)') {
            EmitDecision -Decision 'ask' -Reason "Modification d'une migration, d'un schéma de base ou d'une configuration. Validation humaine requise."
        }
    }

    # 5. Suppression de tests → deny
    if ($toolName -in @('delete','remove')) {
        foreach ($toolArg in $toolArgStrings) {
            if ($toolArg -match '(?i)(^|/)(tests?/|[^/]*\.Tests/|.*\.Tests\.|.*\.spec\.|.*\.test\.)') {
                EmitDecision -Decision 'deny' -Reason 'Suppression de test bloquée. Validation humaine explicite requise.'
            }
        }
    }

    # 6. CI/CD et infrastructure
    foreach ($toolArg in $toolArgStrings) {
        if ($toolArg -match '(?i)(^|/)(\.github/(workflows|actions)/|Dockerfile$|docker-compose.*\.ya?ml$|.*\.(tf|tfvars)$|Chart\.yaml$|Jenkinsfile$|azure-pipelines\.ya?ml$)') {
            EmitDecision -Decision 'ask' -Reason "Modification de CI/CD ou d'infrastructure. Validation humaine requise."
        }
    }

    # 7. Composant Blazor — suppression → ask
    if ($toolName -in @('delete','remove')) {
        if ($toolArgsStrings -match '(?i)\.razor$') {
            EmitDecision -Decision 'ask' -Reason "Suppression d'un composant Blazor. Vérifie références, routes et tests."
        }
    }
}

# ---------------------------------------------------------------
# 8. Fallback : allow
# ---------------------------------------------------------------
EmitDecision -Decision 'allow'