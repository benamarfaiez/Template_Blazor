# Instructions projet — Blazor

## Contexte du projet

- Projet : <MonProjet>
- Type d’application : Blazor <Server | WebAssembly | Auto | Hybrid>
- Framework : .NET <10>
- Langue principale du code et des commentaires : <français>


## Commandes de développement

Utilise ces commandes lorsque cela est pertinent :

```bash
dotnet restore
dotnet build --no-restore
dotnet test 
dotnet test --collect:"XPlat Code Coverage"
dotnet format --verify-no-changes
```

- Ne lance jamais une migration de base de données ou une commande destructive sans validation explicite de l’utilisateur.
- Après toute modification de code C#, Razor ou configuration, propose ou exécute `dotnet build` et `dotnet test`.
- Si un test échoue, corrige d’abord la cause racine ; ne désactive pas un test pour faire passer la CI.

## Architecture cible

Respecte la structure suivante :

```text
src/
  <App>.Client/          # Blazor WebAssembly, si applicable
  <App>.Server/          # Blazor Server, API et hébergement
  <App>.Shared/          # DTO, contrats, constantes partagées
tests/
  <App>.Tests.Unit/      # xUnit, services et logique métier
  <App>.Tests.Components/ # bUnit, composants Razor
  <App>.Tests.E2E/       # Playwright, parcours utilisateurs
docs/
  adr/                   # Architecture Decision Records
```

Sépare clairement :

- **UI** : composants `.razor`, navigation, présentation, événements utilisateur.
- **Application** : cas d’usage, orchestration, validation applicative.
- **Domaine** : entités, règles métier, value objects.
- **Infrastructure** : EF Core, HTTP, fichiers, files, intégrations externes.

## Règles Blazor

- Garde les composants Razor centrés sur la présentation.
- Déplace la logique métier, la validation complexe et les appels externes dans des services injectés.
- Injecte les dépendances via `@inject` ou le constructeur, jamais par localisation de service.
- Préfère les interfaces de service pour faciliter les tests et le remplacement.
- Utilise `CancellationToken` sur les opérations asynchrones longues lorsque le contexte le permet.
- Libère correctement les ressources avec `IDisposable` ou `IAsyncDisposable`.
- Évite les appels bloquants, `Task.Result`, `.Wait()` et `.GetAwaiter().GetResult()` dans le code Blazor.
- Utilise `EventCallback<T>` pour les événements personnalisés.
- Ne mute pas un `[Parameter]` dans le composant enfant.
- Appelle `StateHasChanged()` uniquement lorsque cela est nécessaire, par exemple après une mise à jour d’état hors du cycle de rendu standard.
- Préfère `@key` pour les listes dynamiques afin d’éviter des rendus inutiles.
- Utilise `Virtualize` pour les listes longues.
- Isole le JavaScript interop derrière une interface et un service dédié.
- Valide côté serveur toute donnée reçue, même si elle est validée côté client.

## Règles C# et .NET

- Utilise C# moderne : records pour les DTO immuables, pattern matching, `required`, collection expressions et `TimeProvider` lorsque pertinent.
- Préfère les types explicites lorsque le type n’est pas évident.
- Utilise `async`/`await` de bout en bout ; évite `async void`, sauf pour les gestionnaires d’événements UI lorsque nécessaire.
- Retourne `Task` plutôt que `async Task` quand aucune opération awaitable n’est nécessaire.
- Utilise `ILogger<T>` plutôt que des `Console.WriteLine`.
- Utilise les exceptions du domaine pour les erreurs métier ; ne retourne pas de booléen ambigu pour signaler une erreur.
- Ne capture pas les exceptions sans les journaliser ou les traiter.
- Respecte les règles analyzers et `.editorconfig` existantes.


## Accessibilité

- Respecte WCAG 2.2 niveau AA.
- Fournis des labels accessibles pour chaque champ de formulaire.
- Assure une navigation clavier complète et un focus visible.
- Fournis des messages d’erreur compréhensibles et associés aux champs concernés.
- Utilise des contrastes suffisants et des états de chargement annoncés.
- Ne repose pas une action importante uniquement sur une icône sans libellé ou
  nom accessible.


## Refactoring

Pour toute demande de refactoring :

1. Analyse le périmètre concerné et les dépendances.
2. Identifie le comportement observable à préserver.
3. Ajoute ou confirme les tests avant modification.
4. Refactore par petites étapes, une responsabilité à la fois.
5. Exécute le build et les tests.
6. Documente les changements d’architecture dans une ADR si nécessaire.
7. Ne modifie pas les contrats publics, le schéma de base ou l’authentification
   sans validation explicite.

Privilégie :

- L’extraction de services et de règles métier depuis les composants Razor.
- L’extraction de composants réutilisables lorsque la duplication est avérée.
- Le remplacement du code dupliqué par des helpers ou des composants partagés.
- Les changements incrémentaux plutôt qu’une réécriture complète.
- Les PR courtes, focalisées et faciles à relire.

## Pull requests

Pour chaque PR :

- Un objectif unique et clairement délimité.
- Une description indiquant le problème, la solution, les risques et les tests.
- Les tests correspondants.
- Aucun fichier généré, secret, binaire inutile ou modification hors périmètre.
- Une note explicite si un comportement utilisateur change.
- Une mention des migrations, variables d’environnement ou dépendances à déployer.

## Documentation

- Mets à jour le README si une commande, une configuration ou un flux de
  démarrage change.
- Crée une ADR dans `docs/adr/` pour toute décision architecturale importante.
- Documente les endpoints, modèles et contrats publics lorsqu’ils changent.
- Rédige les messages de commit au format Conventional Commits lorsque le dépôt
  l’utilise déjà.

## Limites de Copilot

- Ne modifie pas les fichiers de CI/CD, les manifests Kubernetes, les scripts de déploiement ou les configurations d’infrastructure sans demande explicite.
- Ne supprime pas de tests, de règles d’analyse ou de mécanismes de sécurité pour résoudre une erreur.
- Ne fais pas d’hypothèse sur les exigences métier : pose une question si le comportement attendu est ambigu.
- Signale explicitement les zones nécessitant une décision humaine.

## Définition de terminé

Une tâche est terminée uniquement si :

- Le build réussit.
- Les tests unitaires, de composants et E2E pertinents passent.
- Le comportement utilisateur existant est préservé, sauf changement explicitement demandé.
- Les erreurs, états de chargement et cas limites sont gérés.
- Les règles de sécurité et d’accessibilité sont respectées.
- La documentation et les migrations nécessaires sont mises à jour.
- Aucun secret, fichier généré ou modification hors périmètre n’est inclus.

## Gestion des erreurs

- Ne masque jamais une erreur.
- Journalise les erreurs avec un contexte utile : identifiant de corrélation,
  utilisateur anonymisé si disponible, opération et cause.
- Retourne à l’utilisateur un message clair, sans détails techniques internes.
- Distingue les erreurs métier attendues des erreurs techniques inattendues.
- Utilise `ProblemDetails` pour les erreurs d’API.
- Pour les opérations critiques, implémente un comportement explicite en cas
  d’échec : retry contrôlé, compensation, file d’attente ou message d’échec.

## Observabilité

- Utilise la journalisation structurée.
- Journalise les événements métier importants : création, modification,
  suppression, authentification et opération sensible.
- N’journalise jamais un mot de passe, token, cookie, numéro de carte,
  donnée de santé ou autre donnée sensible.
- Ajoute des identifiants de corrélation pour suivre une requête de bout en bout.
- Ajoute des métriques pour les opérations lentes, les erreurs et les parcours critiques.

## Performance

- Mesure avant d’optimiser ; ne fait pas d’optimisation prématurée.
- Évite les requêtes N+1, les chargements inutiles et les listes non paginées.
- Préfère les projections EF Core aux entités complètes pour la lecture.
- Utilise `AsNoTracking()` pour les requêtes en lecture seule.
- Parallélise les appels indépendants lorsque cela est sûr.
- Évite les traitements lourds dans le rendu Blazor.
- Utilise `Virtualize` pour les listes longues.
- Surveille la taille du bundle et le temps de démarrage en Blazor WebAssembly.

## Gestion d’état Blazor

- Garde l’état local dans le composant uniquement lorsqu’il est propre à l’UI.
- Utilise un service scoped ou un store dédié pour l’état partagé entre composants.
- Évite les variables statiques mutables pour l’état applicatif.
- Ne stocke pas d’état sensible ou d’objet métier volumineux dans le navigateur.
- Gère explicitement la perte de session, l’expiration d’authentification et le rechargement.
- Utilise des immuables ou des copies défensives lorsque les données sont partagées.

## JavaScript interop

- Isole chaque interop derrière une interface et un service.
- Valide les entrées et sorties JS.
- Gère les erreurs et les cas où JavaScript n’est pas disponible.
- Libère les ressources JS avec `IJSObjectReference.DisposeAsync()`.
- Évite les appels JS pendant le prerendering.
- Préférez les API .NET natives lorsqu’elles existent.

## API HTTP

- Utilise `IHttpClientFactory` plutôt qu’un `HttpClient` créé manuellement.
- Configure un timeout raisonnable pour chaque appel externe.
- Utilise `CancellationToken` et propage-le jusqu’aux appels HTTP.
- Valide les réponses et gère les codes d’erreur HTTP.
- Ne fais pas confiance aux données reçues d’une API externe.
- Utilise Polly ou une politique de résilience équivalente pour retry, circuit
  breaker et timeout, si le projet l’utilise déjà.

## Git et livraison

- Crée une branche par tâche ou incrément de refactoring.
- Un commit doit représenter une intention claire.
- Ne mélange pas refactoring, correction de bug, nouvelle fonctionnalité et
  changement de dépendance dans la même PR.
- Utilise des messages de commit descriptifs.
- Avant de proposer une PR, vérifie build, tests, formatage et analyse statique.