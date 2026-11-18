$ErrorActionPreference = 'Stop'

$input = [Console]::In.ReadToEnd()
$payload = $input | ConvertFrom-Json

$toolName = $payload.toolName
$toolArgs = $payload.toolArgs | ConvertTo-Json -Depth 10 -Compress

# Autoriser explicitement les outils de lecture courants
$readOnlyTools = @('view', 'read', 'search', 'glob', 'grep', 'list_files', 'get_file_contents')

if ($readOnlyTools -contains $toolName) {
    Write-Output '{"permissionDecision":"allow"}'
    exit 0
}

# 1. Commandes shell dangereuses
if ($toolName -eq 'bash' -or $toolName -eq 'shell') {
    $blockedPatterns = @(
        'dotnet ef database drop',
        'dotnet ef database update',
        'dotnet ef migrations remove',
        'dotnet ef migrations add',
        'git push --force',
        'git push -f',
        'git reset --hard',
        'git clean -fd',
        'git clean -fdx',
        'rm -rf',
        'rmdir /s /q',
        'del /s /q',
        'Remove-Item -Recurse -Force',
        'kubectl delete',
        'docker system prune',
        'docker volume rm',
        'helm uninstall',
        'terraform destroy',
        'az group delete'
    )

    foreach ($pattern in $blockedPatterns) {
        if ($toolArgs -match [regex]::Escape($pattern)) {
            $response = @{
                permissionDecision = 'deny'
                permissionDecisionReason = "Commande potentiellement destructive bloquée : $pattern. Demande une validation humaine explicite."
            }

            Write-Output ($response | ConvertTo-Json -Depth 3 -Compress)
            exit 0
        }
    }

    # Bloquer les installations ou scripts externes non validés
    if ($toolArgs -match '(?i)curl .*\|\s*(ba)?sh|wget .*\|\s*(ba)?sh|npm install|pnpm add|yarn add|dotnet tool install|dotnet add package') {
        $response = @{
            permissionDecision = 'ask'
            permissionDecisionReason = 'Installation de dépendance ou exécution de script externe détectée. Validation humaine requise.'
        }

        Write-Output ($response | ConvertTo-Json -Depth 3 -Compress)
        exit 0
    }

    # Autoriser les commandes de build et de test
    if ($toolArgs -match '(?i)dotnet build|dotnet test|dotnet format|dotnet list package|dotnet ef dbcontext script') {
        Write-Output '{"permissionDecision":"allow"}'
        exit 0
    }

    # Demander confirmation pour les autres commandes shell
    $response = @{
        permissionDecision = 'ask'
        permissionDecisionReason = 'Commande shell non classée. Vérifie son impact avant exécution.'
    }

    Write-Output ($response | ConvertTo-Json -Depth 3 -Compress)
    exit 0
}

# 2. Protection des fichiers sensibles
if ($toolArgs -match '(?i)appsettings\.Production\.json|\.env|\.pfx|\.key|secret|credential|password|connectionstring') {
    $response = @{
        permissionDecision = 'ask'
        permissionDecisionReason = 'Fichier potentiellement sensible détecté. Validation humaine requise avant lecture ou modification.'
    }

    Write-Output ($response | ConvertTo-Json -Depth 3 -Compress)
    exit 0
}

# 3. Protection des migrations et de la base de données
if ($toolArgs -match '(?i)Migrations/|migrations|\.edmx|database|connectionstring') {
    if ($toolArgs -match '(?i)delete|remove|drop|update|write|edit') {
        $response = @{
            permissionDecision = 'ask'
            permissionDecisionReason = 'Modification potentielle d une migration, d un schéma de base ou d une configuration de connexion. Validation humaine requise.'
        }

        Write-Output ($response | ConvertTo-Json -Depth 3 -Compress)
        exit 0
    }
}

# 4. Protection des tests
if ($toolArgs -match '(?i)tests?/|\.Tests\.|\.spec\.|\.test\.') {
    if ($toolArgs -match '(?i)delete|remove') {
        $response = @{
            permissionDecision = 'deny'
            permissionDecisionReason = 'Suppression de test bloquée. Les tests ne doivent pas être supprimés sans validation humaine explicite.'
        }

        Write-Output ($response | ConvertTo-Json -Depth 3 -Compress)
        exit 0
    }
}

# 5. Protection des fichiers CI/CD et infrastructure
if ($toolArgs -match '(?i)\.github/workflows/|\.github/actions/|Dockerfile|docker-compose|kubernetes|k8s|terraform|helm|\.azuredevops|Jenkinsfile') {
    $response = @{
        permissionDecision = 'ask'
        permissionDecisionReason = 'Modification de CI/CD ou d infrastructure détectée. Validation humaine requise.'
    }

    Write-Output ($response | ConvertTo-Json -Depth 3 -Compress)
    exit 0
}

# 6. Protection des fichiers Razor critiques
if ($toolArgs -match '(?i)\.razor$') {
    if ($toolArgs -match '(?i)delete|remove') {
        $response = @{
            permissionDecision = 'ask'
            permissionDecisionReason = 'Suppression d un composant Blazor détectée. Vérifie les références, routes et tests avant validation.'
        }

        Write-Output ($response | ConvertTo-Json -Depth 3 -Compress)
        exit 0
    }
}

# Par défaut : autoriser
Write-Output '{"permissionDecision":"allow"}'