# AGENTS.md

## Mission

Aide à maintenir et refactorer une application Blazor .NET de manière sûre, incrémentale et testable. Préserve le comportement utilisateur sauf changement explicitement demandé.

Tu n’es pas autorisé à modifier des données de production, à déployer en production ou à contourner les règles de sécurité sans validation humaine explicite.

## Priorités

1. Sécurité et protection des données.
2. Stabilité et absence de régression.
3. Exactitude fonctionnelle.
4. Testabilité et maintenabilité.
5. Performance raisonnable.
6. Accessibilité et UX.

## Environnement

- .NET 10
- Variante Blazor confirmée par les fichiers projet
- Tests : xUnit, bUnit, Playwright
- Build : `dotnet restore`, puis `dotnet build --no-restore`
- Tests : `dotnet test `

## Règles essentielles

- Sépare UI, application, domaine et infrastructure.
- Garde les composants `.razor` centrés sur la présentation.
- Extrais la logique métier vers des services injectés.
- Utilise des interfaces de service et l’injection par constructeur.
- Utilise `async`/`await` et propage `CancellationToken`.
- Libère les ressources avec `IDisposable` ou `IAsyncDisposable`.
- Valide toute entrée côté serveur.
- Applique l’autorisation côté serveur.
- Utilise `AsNoTracking()` pour les lectures EF Core.
- Évite les requêtes N+1 et pagine les listes volumineuses.
- Écris ou mets à jour les tests pour tout changement de comportement.
- Avant un refactoring, ajoute des tests de caractérisation.
- Fais des PR courtes, focalisées et réversibles.
- Ne mélange pas refactoring, fonctionnalité, bugfix et migration.

## Interdictions

Ne fais pas, sans validation humaine explicite :

- Migration ou suppression de base de données.
- Modification du schéma de base.
- Modification des contrats d’API publics.
- Modification de l’authentification ou des autorisations.
- Modification de CI/CD, Docker, Kubernetes ou Terraform.
- Suppression de tests, règles d’analyse ou mécanismes de sécurité.
- Ajout ou suppression d’une dépendance majeure.
- Push forcé, reset dur ou suppression de fichiers.
- Stockage de secret dans le code ou les fichiers versionnés.

## Séparation des responsabilités

- **Composants `.razor`** : présentation, navigation, événements utilisateur,
  états d’affichage et appels à des services d’application.
- **Services d’application** : cas d’usage, orchestration, validation
  applicative et coordination des dépendances.
- **Domaine** : entités, règles métier, invariants et value objects.
- **Infrastructure** : EF Core, HTTP, fichiers, files d’attente, intégrations
  externes et configuration.
- **Tests** : validation du comportement observable, pas de l’implémentation interne.

Ne place pas de logique métier, de requête SQL, d’appel HTTP ou de règle
d’autorisation directement dans un composant `.razor`.

## Workflow standard

Pour toute tâche de développement ou de refactoring :

1. **Comprendre** : lis les fichiers concernés, les tests, la documentation et les instructions du dépôt.
2. **Clarifier** : identifie les ambiguïtés et pose des questions si le comportement attendu n’est pas clair.
3. **Planifier** : découpe le travail en incréments petits, indépendants et réversibles.
4. **Tester d’abord** : ajoute ou confirme les tests qui décrivent le
   comportement actuel avant de modifier le code.
5. **Modifier** : applique une seule responsabilité ou un seul objectif par PR.
6. **Vérifier** : exécute le build et les tests pertinents.
7. **Documenter** : mets à jour README, ADR, changelog ou documentation API si nécessaire.
8. **Proposer** : crée une PR claire avec objectif, changements, risques, tests et instructions de déploiement.

## Definition of Done

- Build vert.
- Tests pertinents verts.
- Comportement utilisateur préservé.
- Erreurs, chargement et cas limites gérés.
- Sécurité et accessibilité respectées.
- Documentation et migration mises à jour si nécessaire.
- PR claire avec objectif, risques, tests et rollback.

## Format des rapports d’audit

Pour un audit, produis un rapport Markdown avec :

1. Synthèse exécutive.
2. Cartographie du projet.
3. Constats classés par sévérité : `bloquant`, `élevé`, `moyen`, `faible`.
4. Preuves avec fichier et ligne.
5. Plan de refactoring incrémental.
6. Tests recommandés.
7. Questions à clarifier.

Ne présente jamais une hypothèse comme un fait. Distingue clairement :

- **Constat vérifié** : observé dans le code ou la configuration.
- **Hypothèse à confirmer** : probable, mais nécessitant une validation.
- **Recommandation** : action proposée, non encore décidée.

## Collaboration humaine

Demande une validation humaine avant de :

- Modifier une règle d’autorisation.
- Modifier une migration ou le schéma de base.
- Modifier un contrat d’API public.
- Ajouter, mettre à jour ou supprimer une dépendance majeure.
- Modifier la CI/CD ou l’infrastructure.
- Supprimer un test, une règle d’analyse ou un mécanisme de sécurité.
- Effectuer une migration de données ou une opération destructive.
- Fusionner une PR.

## Références du dépôt

- Instructions générales : `.github/copilot-instructions.md`
- Règles Razor : `.github/instructions/blazor-razor.instructions.md`
- Règles C# : `.github/instructions/csharp.instructions.md`
- Règles EF Core : `.github/instructions/entity-framework.instructions.md`
- Règles de tests : `.github/instructions/testing.instructions.md`
- Règles de revue : `.github/instructions/code-review.instructions.md`
- Règles de sécurité : `.github/instructions/security.instructions.md`
- Skills communs : `.github/skills/`
- Glossaire commun : `.github/skills/glossary.md`

## Agents disponibles

- `.github/agents/blazor-architect.md`
- `.github/agents/blazor-auditor.md`
- `.github/agents/blazor-code-review.md`
- `.github/agents/blazor-test-engineer.md`
- `.github/agents/blazor-qa-agent.md`
- `.github/agents/blazor-refactor-planner.md`
- `.github/agents/blazor-refactor-executor.md`
- `.github/agents/blazor-refactoring-agent.md`
