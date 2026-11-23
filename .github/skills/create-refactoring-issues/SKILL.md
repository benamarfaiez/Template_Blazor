---
description: Crée des issues GitHub exploitables à partir d'un plan de refactoring Blazor validé. Une issue par incrément, avec périmètre, tests, risques et rollback.
allowed-tools:
  - read
  - search
  - github-mcp/list_issues
  - github-mcp/get_issue
  - github-mcp/create_issue
name: create-refactoring-issues
---

# Create Refactoring Issues

## Description
Crée des issues GitHub exploitables à partir d'un plan de refactoring Blazor validé. Une issue par incrément, avec périmètre, tests, risques et rollback.

## When to use
- Invoquer pour : Crée des issues GitHub exploitables à partir d'un plan de refactoring Blazor validé. Une issue par incrément, avec périmètre, tests, risques et rollback.
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
1. Vérifie les issues existantes en limitant la recherche à 10 résultats.
2. Vérifie que le plan est validé.
3. Vérifie chaque incrément : périmètre, objectif, comportement, tests, risques, dépendances et rollback.
4. Arrête la création si une migration ou décision humaine reste non validée.
5. Crée une issue par incrément, dans l’ordre de priorité.
6. Référence les dépendances entre issues.

Utilise ce format :

```text
Titre : Refactoring Blazor : <action précise>
Objectif : <résultat observable>
Périmètre : <chemins exacts>
Comportement à préserver : <preuves existantes>
Tests préalables : <xUnit, bUnit ou Playwright>
Critères : dotnet build --no-restore; tests pertinents; aucun contrat public modifié
Risques : <risque> | <impact> | <mitigation>
Rollback : <revert ou restauration>
Dépendances : <issues ou décisions>
```

## Exemples
- Exemple : appliquer le skill à une cible nommée explicitement par l’appelant.
- Exemple : produire une sortie vérifiée avec chemins exacts et commandes exécutées.

## Limites
- Inspecter au plus 20 fichiers sans exploration exhaustive.
- Arrêter après 3 tentatives ciblées infructueuses.
- Consulter `../glossary.md` pour les termes communs.
