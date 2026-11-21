---
description: Prépare une pull request Blazor courte, focalisée et réversible après un refactoring validé, avec description, tests, risques et rollback.
allowed-tools:
  - read
  - search
  - glob
  - grep
  - terminal
  - github-mcp/create_pull_request
name: prepare-refactoring-pull-request
---

# Prepare Refactoring Pull Request

## Description
Prépare une pull request Blazor courte, focalisée et réversible après un refactoring validé, avec description, tests, risques et rollback.

## When to use
- Invoquer pour : Prépare une pull request Blazor courte, focalisée et réversible après un refactoring validé, avec description, tests, risques et rollback.
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
Prépare une PR après un refactoring validé.

## Conditions préalables

Ne prépare pas de PR si :

- Le build échoue.
- Un test pertinent échoue.
- Une régression est détectée.
- Un bug existant n’a pas été traité ou accepté explicitement.
- Une migration ou un déploiement est nécessaire sans validation humaine.
- Le périmètre dépasse l’incrément prévu.

## Méthode

1. Vérifie le build et les tests.
2. Vérifie le formatage.
3. Vérifie les fichiers modifiés.
4. Vérifie l’absence de secret.
5. Vérifie l’absence de migration non validée.
6. Crée la PR avec une description complète.

## Format de PR

```md
## Objectif

<Problème résolu ou amélioration apportée.>

## Changements

- <Changement 1>
- <Changement 2>

## Périmètre

- Inclus : <fichiers ou modules>
- Exclus : <fichiers ou modules volontairement non modifiés>

## Comportement préservé

- <Comportement observable 1>
- <Comportement observable 2>

## Tests

- Tests ajoutés : <liste>
- Tests mis à jour : <liste>
- Commande exécutée : `dotnet test `

## Risques

- <Risque principal et mitigation>

## Déploiement

- Migration : <oui/non>
- Variable d’environnement : <oui/non>
- Changement de configuration : <oui/non>
- Rollback : <procédure>
```

## Règles

- Une PR par incrément.
- Ne fusionne jamais la PR.
- Ne déploie jamais.
- Ne modifie pas le code après la création de la PR sans validation.
- Ne stocke jamais de secret dans la description ou les fichiers modifiés.

## Exemples
- Exemple : appliquer le skill à une cible nommée explicitement par l’appelant.
- Exemple : produire une sortie vérifiée avec chemins exacts et commandes exécutées.

## Limites
- Inspecter au plus 20 fichiers sans exploration exhaustive.
- Arrêter après 3 tentatives ciblées infructueuses.
- Consulter `../glossary.md` pour les termes communs.
