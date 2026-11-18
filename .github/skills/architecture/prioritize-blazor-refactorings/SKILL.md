---
name: prioritize-blazor-refactorings
description: Priorise les refactorings Blazor identifiés en plan incrémental, avec tests, risques, effort et rollback.
allowed-tools:
  - read
  - search
  - github-mcp/create_issue
  - github-mcp/list_issues
---

# Prioriser les refactorings Blazor

Transforme les refactorings identifiés en plan d’exécution priorisé.

## Priorisation

Classe chaque action :

- `P0` : sécurité, perte de données, bug critique ou régression majeure
- `P1` : risque de production, testabilité insuffisante ou performance dégradée
- `P2` : maintenabilité, duplication, UX ou robustesse
- `P3` : style, nommage ou documentation

## Règles de découpage

- Une action par responsabilité.
- Une PR courte, indépendante et réversible.
- Pas de mélange entre refactoring, nouvelle fonctionnalité, correction de bug
  et migration de base.
- Tests de caractérisation avant toute modification de comportement.
- Rollback explicite pour chaque incrément.
- Pas de réécriture complète sans justification très forte.

## Format du plan

| Priorité | Refactoring | Périmètre | Bénéfice | Risque | Tests préalables | Effort | Rollback |
|---|---|---|---|---|---|---|---|
| P0 | Extraire `OrderValidator` | `Pages/Orders/OrderEdit.razor`, `Services/Orders` | Testabilité | Faible | bUnit + xUnit | S | Revert de la PR |

## Critères d’acceptation

Pour chaque incrément, définis :

- Comportement à préserver
- Tests à ajouter avant modification
- Tests à mettre à jour
- Critères de succès
- Risques et mitigations
- Procédure de rollback
- Dépendances avec d’autres incréments

## Règles

- Propose 5 à 10 incréments maximum.
- Ne crée une issue GitHub que si l’utilisateur le demande explicitement.
- Ne crée pas de PR.
- Ne modifie pas le code.
- Ne propose pas de migration de base sans validation humaine.
- Indique explicitement les décisions nécessitant un humain.