$ErrorActionPreference = 'Stop'

$input = [Console]::In.ReadToEnd()
$payload = $input | ConvertFrom-Json
$prompt = $payload.prompt

if (-not $prompt) {
    Write-Output '{}'
    exit 0
}

# Sécurité : refuser les demandes destructives explicites
$dangerousPatterns = @(
    'drop database',
    'supprimer la base',
    'drop migration',
    'supprimer les migrations',
    'force push',
    'git reset --hard',
    'rm -rf'
)

foreach ($pattern in $dangerousPatterns) {
    if ($prompt -match [regex]::Escape($pattern)) {
        $response = @{
            decision = 'deny'
            reason   = "Demande potentiellement destructive. Demande une validation humaine explicite avant toute action."
        }

        Write-Output ($response | ConvertTo-Json -Depth 3 -Compress)
        exit 0
    }
}

$context = @()

# 1. Refactoring
if ($prompt -match '(?i)refactor|refactoring|nettoyer|clean up|optimiser|optimization|simplifier') {
    $context += @(
        "Règles de refactoring Blazor :",
        "- Préserve strictement le comportement utilisateur observable.",
        "- Ajoute ou confirme d'abord les tests de caractérisation.",
        "- Refactore par petites PR indépendantes et réversibles.",
        "- Extrais la logique métier des composants Razor vers des services testables.",
        "- Ne mélange pas refactoring, nouvelle fonctionnalité, correction de bug et migration.",
        "- Exécute dotnet build --no-restore et les tests pertinents avant de proposer une PR."
    )
}

# 2. Composant Razor
if ($prompt -match '(?i)\.razor|composant blazor|component|page blazor|blazor page') {
    $context += @(
        "Règles pour les composants Blazor :",
        "- Garde le composant centré sur la présentation et l'interaction utilisateur.",
        "- Extrais la logique métier, la validation complexe et les appels externes vers des services.",
        "- Traite les paramètres [Parameter] comme immuables.",
        "- Utilise EventCallback<T> pour les événements personnalisés.",
        "- Gère explicitement les états de chargement, vide, erreur et succès.",
        "- Utilise @key pour les listes dynamiques et Virtualize pour les listes longues.",
        "- Respecte l'accessibilité : labels, navigation clavier, focus visible et messages d'erreur."
    )
}

# 3. Tests
if ($prompt -match '(?i)test|bunit|playwright|xunit|couverture|coverage') {
    $context += @(
        "Règles de tests Blazor :",
        "- Utilise xUnit pour les services et la logique métier.",
        "- Utilise bUnit pour les composants Razor.",
        "- Utilise Playwright pour les parcours utilisateurs critiques.",
        "- Écris des tests déterministes, isolés et rapides.",
        "- Teste le comportement observable, pas les détails d'implémentation.",
        "- Ne supprime ou ne désactive jamais un test pour faire passer la suite.",
        "- Pour un refactoring, commence par des tests de caractérisation."
    )
}

# 4. Entity Framework Core et migrations
if ($prompt -match '(?i)ef core|entity framework|migration|database|base de données|dbcontext') {
    $context += @(
        "Règles Entity Framework Core :",
        "- Utilise DbContext avec une durée de vie Scoped.",
        "- Utilise AsNoTracking() pour les requêtes en lecture seule.",
        "- Évite les requêtes N+1 et pagine les listes volumineuses.",
        "- Utilise des projections pour limiter les données chargées.",
        "- N'expose pas les entités EF directement à l'UI ou dans les réponses API.",
        "- Isole les migrations dans une PR dédiée.",
        "- Ne lance aucune migration, suppression de base ou suppression de migration sans validation humaine explicite."
    )
}

# 5. Sécurité
if ($prompt -match '(?i)authentification|authorization|autorisation|sécurité|security|secret|token|api key|login|jwt') {
    $context += @(
        "Règles de sécurité Blazor :",
        "- Applique l'autorisation côté serveur, même si l'UI masque une action.",
        "- Utilise les politiques d'autorisation ASP.NET Core.",
        "- Valide toute entrée utilisateur et toute donnée externe.",
        "- Ne stocke jamais de secret, token, mot de passe ou chaîne de connexion dans le code ou les fichiers versionnés.",
        "- Utilise des requêtes paramétrées pour prévenir l'injection SQL.",
        "- Encode le contenu utilisateur avant rendu HTML.",
        "- Ne journalise pas de mot de passe, token, cookie ou donnée personnelle sensible."
    )
}

# 6. Performance
if ($prompt -match '(?i)performance|lent|slow|latence|rendu|render|virtualize|pagination|cache') {
    $context += @(
        "Règles de performance Blazor :",
        "- Mesure avant d'optimiser ; évite l'optimisation prématurée.",
        "- Évite les requêtes N+1, les chargements inutiles et les listes non paginées.",
        "- Utilise AsNoTracking() et des projections pour les lectures EF Core.",
        "- Utilise Virtualize pour les listes longues.",
        "- Utilise @key pour les éléments de liste dynamiques.",
        "- Évite les traitements coûteux dans le markup Razor.",
        "- Propage CancellationToken dans les opérations asynchrones longues."
    )
}

# 7. Accessibilité
if ($prompt -match '(?i)accessibilité|accessibility|a11y|wcag|formulaire|form|ui|interface') {
    $context += @(
        "Règles d'accessibilité Blazor :",
        "- Respecte WCAG 2.2 niveau AA.",
        "- Associe chaque champ de formulaire à un label accessible.",
        "- Assure une navigation clavier complète et un focus visible.",
        "- Fournis des messages d'erreur clairs et associés aux champs concernés.",
        "- Utilise des contrastes suffisants et des états de chargement annoncés.",
        "- Ne repose pas une action importante uniquement sur une icône."
    )
}

# 8. JavaScript interop
if ($prompt -match '(?i)javascript|js interop|ijsruntime|interop') {
    $context += @(
        "Règles JavaScript interop Blazor :",
        "- Isole chaque interop derrière une interface et un service dédié.",
        "- Valide les entrées et sorties JS.",
        "- Gère les erreurs et les cas où JavaScript n'est pas disponible.",
        "- Libère les ressources JS avec IJSObjectReference.DisposeAsync().",
        "- Évite les appels JS pendant le prerendering.",
        "- Préfère les API .NET natives lorsqu'elles existent."
    )
}

# 9. API HTTP
if ($prompt -match '(?i)httpclient|api|http|rest|graphql|signalr|websocket') {
    $context += @(
        "Règles d'intégration Blazor :",
        "- Utilise IHttpClientFactory plutôt qu'un HttpClient créé manuellement.",
        "- Configure un timeout raisonnable pour chaque appel externe.",
        "- Propage CancellationToken jusqu'aux appels HTTP.",
        "- Valide les réponses et gère les codes d'erreur HTTP.",
        "- Ne fais pas confiance aux données reçues d'une API externe.",
        "- Utilise une politique de résilience pour retry, circuit breaker et timeout si le projet l'utilise déjà."
    )
}

# 10. Pull request
if ($prompt -match '(?i)pull request|pr|merge|fusion|commit') {
    $context += @(
        "Règles de pull request :",
        "- Une PR doit avoir un objectif unique et clairement délimité.",
        "- Décris le problème, la solution, les risques, les tests et le rollback.",
        "- N'inclus aucun fichier généré, secret ou modification hors périmètre.",
        "- Mentionne les migrations, variables d'environnement et dépendances à déployer.",
        "- Ne fusionne jamais une PR sans validation humaine."
    )
}

# Si aucune règle spécifique, injecter les règles minimales Blazor
if ($context.Count -eq 0) {
    $context += @(
        "Contexte projet Blazor :",
        "- Préserve le comportement utilisateur observable.",
        "- Sépare UI, application, domaine et infrastructure.",
        "- Ajoute ou met à jour les tests pour tout changement de comportement.",
        "- Respecte la sécurité, l'accessibilité et les conventions du dépôt.",
        "- Demande une clarification si le comportement attendu est ambigu."
    )
}

$contextText = $context -join "`n"

$response = @{
    additionalContext = $contextText
}

Write-Output ($response | ConvertTo-Json -Depth 3 -Compress)