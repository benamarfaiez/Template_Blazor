---
description: Produit une revue de pull request Blazor structurée, priorisée et actionnable, avec décision finale claire.
allowed-tools:
  - read
  - search
  - github-mcp/get_pull_request
  - github-mcp/get_pull_request_diff
  - github-mcp/create_pending_pull_request_review
  - github-mcp/add_comment_to_pending_pull_request_review
  - github-mcp/submit_pending_pull_request_review
name: code-review-blazor-report
---

# Code Review Report

## Description
Produit une revue de pull request Blazor structurée, priorisée et actionnable, avec décision finale claire.

## When to use
- Invoquer pour : Produit une revue de pull request Blazor structurée, priorisée et actionnable, avec décision finale claire.
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
Produis une revue structurée à partir des constats des autres skills.

## Règles de priorisation

Classe chaque commentaire :

- `BLOQUANT` : sécurité, perte de données, régression majeure, bug critique ou test insuffisant sur un risque élevé
- `IMPORTANT` : risque de production, maintenabilité, performance ou accessibilité significative
- `SUGGESTION` : lisibilité, style, nommage ou amélioration facultative

## Règles de rédaction

- Un commentaire par problème.
- Une localisation précise : fichier et ligne.
- Une explication concise du problème.
- Une correction actionnable.
- Un test recommandé lorsque pertinent.
- Pas de refactoring hors périmètre.
- Pas de commentaire générique ou non vérifiable.
- Pas de divulgation de secret.

## Format du rapport

Produis un objet JSON valide, sans texte avant ou après, avec ces propriétés :

- `summary` : résumé textuel incluant le périmètre, les tests et les questions
  à clarifier.
- `approved` : booléen vrai uniquement si `decision` vaut `APPROVE`.
- `findings` : tableau de chaînes. Chaque constat indique sa sévérité, son
  fichier et sa ligne, le problème et une correction actionnable. Utilise un
  tableau vide s’il n’y a aucun constat.
- `decision` : `APPROVE`, `REQUEST_CHANGES`, `COMMENTER` ou `BLOQUER`.
- Le résumé indique la couverture des fichiers modifiés par les spécialistes
  applicables, y compris les fichiers qui restent sans couverture.

Mappe les décisions vers GitHub ainsi :

- `APPROVE` : `approved: true`, événement `APPROVE`.
- `REQUEST_CHANGES` : `approved: false`, événement `REQUEST_CHANGES`.
- `COMMENTER` : `approved: false`, événement `COMMENT`.
- `BLOQUER` : `approved: false`, événement `REQUEST_CHANGES`.

Exemple :

```json
{
  "summary": "Revue terminée. Tests : couverture insuffisante sur le parcours modifié. Questions : aucune.",
  "approved": false,
  "findings": [
    "[IMPORTANT] `src/Example.cs:42` — La validation est absente. Ajouter une validation côté serveur et couvrir le cas par un test."
  ],
  "decision": "REQUEST_CHANGES"
}
```

## Décision

- `BLOQUER` : la revue contient une faille de sécurité, une perte de données potentielle,
  une régression majeure ou un changement dangereux non validé.
- `REQUEST_CHANGES` : appliquer uniquement si aucun critère `BLOQUER` n’est atteint,
  mais qu’au moins un point `BLOQUANT` ou `IMPORTANT` est vérifié ou qu’un fichier
  modifié reste sans couverture.
- `APPROVE` : appliquer uniquement si aucun point `BLOQUANT`, `IMPORTANT` ou
  `BLOQUER` n’est vérifié et que la couverture est suffisante. La couverture est
  suffisante uniquement lorsque tous les fichiers modifiés ont été examinés par
  les spécialistes applicables.
- `COMMENTER` : appliquer uniquement si aucun point `BLOQUANT`, `IMPORTANT` ou
  `BLOQUER` n’est vérifié et qu’au moins un fichier modifié reste sans couverture.
  Si un tel fichier reste sans couverture, la décision ne peut pas être `APPROVE` :
  utiliser `COMMENTER` ou `REQUEST_CHANGES` selon les constats vérifiés.

## Exemples
- Exemple : appliquer le skill à une cible nommée explicitement par l’appelant.
- Exemple : produire une sortie vérifiée avec chemins exacts et commandes exécutées.

## Limites
- Inspecter au plus 20 fichiers sans exploration exhaustive.
- Arrêter après 3 tentatives ciblées infructueuses.
- Consulter `../glossary.md` pour les termes communs.
