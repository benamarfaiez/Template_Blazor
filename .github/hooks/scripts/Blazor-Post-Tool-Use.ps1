$ErrorActionPreference = 'Stop'

$input = [Console]::In.ReadToEnd()
$payload = $input | ConvertFrom-Json

$toolName = $payload.toolName
$toolArgs = $payload.toolArgs | ConvertTo-Json -Depth 10 -Compress
$toolError = $payload.error

$context = @()

# 1. Après une édition de code
if ($toolName -eq 'edit' -or $toolName -eq 'write') {
    $changedFiles = git diff --name-only --diff-filter=ACMR 2>$null

    if ($changedFiles -match '\.(cs|razor)$') {
        $context += @(
            "Un fichier C# ou Razor a été modifié.",
            "Avant de proposer une PR :",
            "- Exécute dotnet build --no-restore.",
            "- Exécute les tests pertinents avec dotnet test --no-build.",
            "- Vérifie le formatage avec dotnet format --verify-no-changes.",
            "- Pour un composant Razor, vérifie les états de chargement, vide, erreur et succès.",
            "- Pour un service, vérifie la validation, les erreurs métier et les cas limites."
        )
    }

    if ($changedFiles -match '\.razor$') {
        $context += @(
            "Un composant Blazor a été modifié.",
            "Vérifie :",
            "- La séparation UI / logique métier.",
            "- Les paramètres immuables et EventCallback<T>.",
            "- La libération des ressources avec IDisposable ou IAsyncDisposable.",
            "- La propagation de CancellationToken pour les opérations longues.",
            "- L'accessibilité : labels, navigation clavier, focus et messages d'erreur.",
            "- Les tests bUnit couvrant le rendu et les interactions."
        )
    }

    if ($changedFiles -match '\.csproj$') {
        $context += @(
            "Un fichier projet .csproj a été modifié.",
            "Vérifie :",
            "- La nécessité réelle de la dépendance ajoutée.",
            "- Les vulnérabilités avec dotnet list package --vulnerable --include-transitive.",
            "- La compatibilité avec la version .NET du projet.",
            "- L'impact sur le bundle et le temps de démarrage en Blazor WebAssembly."
        )
    }

    if ($changedFiles -match 'appsettings.*\.json|\.env|\.yml|\.yaml') {
        $context += @(
            "Un fichier de configuration a été modifié.",
            "Vérifie :",
            "- Aucun secret, token, mot de passe ou chaîne de connexion n'est exposé.",
            "- Les valeurs sensibles proviennent de variables d'environnement ou d'un gestionnaire de secrets.",
            "- Les changements sont documentés et compatibles avec les environnements de déploiement."
        )
    }

    if ($changedFiles -match 'Migrations/|\.edmx') {
        $context += @(
            "Une migration ou un modèle de base de données a été modifié.",
            "ALERTE : une migration ne doit jamais être appliquée automatiquement.",
            "Demande une validation humaine avant dotnet ef database update.",
            "Vérifie la réversibilité, les données existantes et le plan de rollback."
        )
    }

    if ($changedFiles -match 'tests?/|\.Tests\.|\.spec\.|\.test\.') {
        $context += @(
            "Un test a été modifié ou ajouté.",
            "Exécute les tests ciblés et vérifie qu'ils valident le comportement observable,",
            "pas les détails d'implémentation."
        )
    }

    if ($changedFiles -match '\.github/workflows/|Dockerfile|docker-compose|kubernetes|terraform') {
        $context += @(
            "ALERTE : un fichier CI/CD ou d'infrastructure a été modifié.",
            "Ne déploie rien et ne fusionne rien sans validation humaine explicite.",
            "Vérifie les secrets, les permissions, les environnements et le rollback."
        )
    }
}

# 2. Après une commande shell
if ($toolName -eq 'bash' -or $toolName -eq 'shell') {
    if ($toolArgs -match '(?i)dotnet build') {
        if ($toolError) {
            $context += @(
                "Le build a échoué.",
                "- Analyse les premières erreurs du compilateur.",
                "- Ne désactive pas une règle d'analyse ou un test pour masquer l'échec.",
                "- Corrige la cause racine ou demande une clarification."
            )
        } else {
            $context += @(
                "Le build a réussi.",
                "- Exécute maintenant les tests pertinents avec dotnet test --no-build.",
                "- Vérifie le formatage avec dotnet format --verify-no-changes."
            )
        }
    }

    if ($toolArgs -match '(?i)dotnet test') {
        if ($toolError) {
            $context += @(
                "Des tests ont échoué.",
                "- Classe chaque échec : bug existant, régression ou problème de test.",
                "- Ne supprime ou ne désactive pas un test pour faire passer la suite.",
                "- Corrige la cause racine ou demande une clarification."
            )
        } else {
            $context += @(
                "Les tests ont réussi.",
                "- Vérifie que les tests couvrent le comportement modifié.",
                "- Pour un refactoring, vérifie que des tests de caractérisation existent."
            )
        }
    }

    if ($toolArgs -match '(?i)dotnet list package') {
        if ($toolError) {
            $context += @(
                "La vérification des packages a échoué.",
                "- Analyse les vulnérabilités ou erreurs signalées.",
                "- Ne mets pas à jour une dépendance majeure sans validation humaine."
            )
        }
    }
}

# 3. Erreur d'outil
if ($toolError) {
    $context += @(
        "L'outil a retourné une erreur.",
        "Ne relance pas aveuglément la même action.",
        "Analyse la cause racine et demande une clarification si nécessaire."
    )
}

if ($context.Count -eq 0) {
    Write-Output '{}'
    exit 0
}

$contextText = $context -join "`n"

$response = @{
    additionalContext = $contextText
}

Write-Output ($response | ConvertTo-Json -Depth 3 -Compress)