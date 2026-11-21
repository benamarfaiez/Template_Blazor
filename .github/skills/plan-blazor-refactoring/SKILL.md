---
description: Transforme les constats d'audit Blazor en plan de refactoring incrémental, priorisé, testable et réversible.
allowed-tools:
  - read
  - search
  - github-mcp/create_issue
  - github-mcp/list_issues
name: plan-blazor-refactoring
---

# Plan Refactoring

## Description
Transforme les constats d'audit Blazor en plan de refactoring incrémental, priorisé, testable et réversible.

## When to use
- Invoquer pour : Transforme les constats d'audit Blazor en plan de refactoring incrémental, priorisé, testable et réversible.
- Utiliser uniquement lorsque la cible correspond au périmètre du skill.

## Inputs
- Cible, PR, issue ou chemins fournis par l’appelant.
- Contraintes humaines explicites et état Git vérifié.
- Périmètre limité à 20 fichiers au premier passage.

## Outputs
- Résultat vérifié correspondant à la mission du skill.
- Chemins exacts, preuves et décisions humaines restantes.
- Informations inconnues signalées par `À confirmer`.

## Steps
Transforme les constats d’audit en plan d’action incrémental.

## Règles de découpage

- Une action par responsabilité.
- Une PR courte, indépendante et réversible.
- Pas de mélange entre refactoring, nouvelle fonctionnalité, correction de bug
  et migration de base.
- Tests de caractérisation avant toute modification de comportement.
- Rollback explicite pour chaque incrément.
- Pas de réécriture complète sans justification très forte.

## Priorisation

Classe chaque action :

- `P0` : sécurité, perte de données, bug critique ou régression majeure
- `P1` : risque de production, testabilité insuffisante ou performance dégradée
- `P2` : maintenabilité, duplication, UX ou robustesse
- `P3` : amélioration de style, nommage ou documentation

## Format du plan

| Priorité | Incrément | Périmètre | Bénéfice | Risque | Tests préalables | Effort | Rollback |
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

## Exemples
- Exemple : appliquer le skill à une cible nommée explicitement par l’appelant.
- Exemple : produire une sortie vérifiée avec chemins exacts et commandes exécutées.

## Limites
- Inspecter au plus 20 fichiers sans exploration exhaustive.
- Arrêter après 3 tentatives ciblées infructueuses.
- Consulter `../glossary.md` pour les termes communs.
