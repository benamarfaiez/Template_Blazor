---
description: Définit les critères d'acceptation, les tests préalables, les risques, les dépendances et le rollback pour chaque incrément de refactoring Blazor.
allowed-tools:
  - read
  - search
  - glob
  - grep
name: define-refactoring-acceptance-criteria
---

# Define Refactoring Acceptance Criteria

## Description
Définit les critères d'acceptation, les tests préalables, les risques, les dépendances et le rollback pour chaque incrément de refactoring Blazor.

## When to use
- Invoquer pour : Définit les critères d'acceptation, les tests préalables, les risques, les dépendances et le rollback pour chaque incrément de refactoring Blazor.
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
1. Liste le comportement observable à préserver.
2. Liste les fichiers à modifier, créer ou exclure.
3. Confirme les tests xUnit, bUnit ou Playwright préalables.
4. Écris des critères vérifiables par build, test ou comparaison.
5. Classe les risques avec impact, mitigation et test.
6. Liste les dépendances et décisions humaines.
7. Écris une procédure de rollback mesurable.

Utilise la matrice suivante :

| Élément | Valeur attendue |
|---|---|
| Comportement | Résultat utilisateur actuel |
| Périmètre | Chemins exacts |
| Tests | Commandes et assertions |
| Succès | Build, tests et régression |
| Risque | Impact, mitigation, validation |
| Rollback | Revert ou restauration exacte |

- Ne modifie aucun fichier.
- Ne crée pas d’issue.
- Ne crée pas de PR.
- Ne propose pas de réécriture complète.
- Ne mélange pas plusieurs responsabilités dans un incrément.
- Un critère doit être vérifiable par un test, une commande ou une observation claire.

## Exemples
- Exemple : appliquer le skill à une cible nommée explicitement par l’appelant.
- Exemple : produire une sortie vérifiée avec chemins exacts et commandes exécutées.

## Limites
- Inspecter au plus 20 fichiers sans exploration exhaustive.
- Arrêter après 3 tentatives ciblées infructueuses.
- Consulter `../../common/glossary.md` pour les termes communs.
