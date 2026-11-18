---
name: create-refactoring-issues
description: Crée des issues GitHub exploitables à partir d'un plan de refactoring Blazor validé. Une issue par incrément, avec périmètre, tests, risques et rollback.
allowed-tools:
  - read
  - search
  - github-mcp/list_issues
  - github-mcp/get_issue
  - github-mcp/create_issue
---

# Créer les issues de refactoring

Crée une issue GitHub par incrément de refactoring, uniquement après validation
du plan et sur demande explicite de l’utilisateur.

## Règles de création

- Une issue par incrément.
- Un titre court, actionnable et spécifique.
- Une description complète et autonome.
- Pas de duplication avec une issue existante.
- Pas de création d’issue pour une décision humaine non tranchée.
- Pas de création d’issue pour une migration sans validation humaine.
- Pas de création d’issue pour un refactoring non priorisé.

## Vérifications avant création

1. Vérifie les issues existantes pour éviter les doublons.
2. Vérifie que le plan est valide.
3. Vérifie que chaque incrément possède :
   - Périmètre
   - Objectif
   - Comportement à préserver
   - Tests préalables
   - Critères d’acceptation
   - Risques et mitigations
   - Dépendances
   - Rollback
4. Crée les issues dans l’ordre de priorité.
5. Ajoute les références entre issues lorsque des dépendances existent.

## Format du titre

```text
Refactoring Blazor : <action précise>
```

Exemples :

```text
Refactoring Blazor : extraire OrderValidator de OrderEdit.razor
Refactoring Blazor : extraire OrderSummaryCard réutilisable
Refactoring Blazor : ajouter des tests de caractérisation pour OrderService
Refactoring Blazor : remplacer l'accès direct à DbContext dans OrderEdit.razor
```

## Format du corps

```md
## Objectif

<Problème résolu ou amélioration apportée.>

## Contexte

<Référence à l’audit, à la cartographie ou à la demande initiale.>

## Périmètre

### Fichiers à modifier

- `chemin/du/fichier`

### Fichiers à créer

- `chemin/du/nouveau/fichier`

### Fichiers à ne pas modifier

- `chemin/du/fichier`

## Comportement à préserver

- <Comportement observable 1>
- <Comportement observable 2>

## Approche proposée

1. <Étape 1>
2. <Étape 2>
3. <Étape 3>

## Tests préalables

- [ ] Tests xUnit : <détail>
- [ ] Tests bUnit : <détail>
- [ ] Tests Playwright : <détail>

## Critères d’acceptation

- [ ] `dotnet build --no-restore` réussit.
- [ ] Les tests pertinents réussissent.
- [ ] Le comportement utilisateur existant est préservé.
- [ ] La logique métier est extraite du composant Razor.
- [ ] Aucun contrat public n’est modifié.
- [ ] Aucun secret n’est introduit.
- [ ] La PR est limitée au périmètre défini.

## Risques

| Risque | Impact | Mitigation |
|---|---|---|
| <Risque> | <Impact> | <Mitigation> |

## Dépendances

- Bloqué par : <issue ou décision>
- Bloque : <issue ou décision>
- Peut être réalisé en parallèle avec : <issue>

## Rollback

- Revert de la PR associée.
- <Action complémentaire si nécessaire.>

## Décisions humaines

- <Décision à valider avant exécution, si applicable.>
```

## Règles

- Ne crée pas de PR.
- Ne modifie pas le code.
- Ne crée pas d’issue pour une action non planifiée.
- Ne crée pas d’issue pour une migration sans validation humaine.
- Ne crée pas d’issue en masse sans confirmation explicite.
- Ajoute un lien vers les issues dépendantes lorsque cela est possible.