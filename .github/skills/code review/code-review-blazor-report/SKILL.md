---
name: code-review-blazor-report
description: Produit une revue de pull request Blazor structurée, priorisée et actionnable, avec décision finale claire.
allowed-tools:
  - read
  - search
  - github-mcp/get_pull_request
  - github-mcp/get_pull_request_diff
  - github-mcp/create_pending_pull_request_review
  - github-mcp/add_comment_to_pending_pull_request_review
  - github-mcp/submit_pending_pull_request_review
---

# Produire la revue Blazor

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

```md
# Revue de la PR

## Résumé

<Résumé de l’objectif, du périmètre et de l’impact de la PR.>

## Décision

`APPROUVER` | `DEMANDER DES MODIFICATIONS` | `BLOQUER` | `COMMENTER`

## Points bloquants

- <Problème bloquant, fichier, ligne et correction recommandée>

## Points importants

- <Problème important, fichier, ligne et correction recommandée>

## Suggestions

- <Suggestion facultative>

## Tests

- Tests ajoutés ou modifiés
- Tests manquants
- Risque de régression résiduel

## Questions

- <Question à clarifier, si nécessaire>
```

## Décision

- `APPROUVER` : aucun point bloquant et couverture suffisante.
- `DEMANDER DES MODIFICATIONS` : au moins un point bloquant ou important.
- `BLOQUER` : faille de sécurité, perte de données potentielle, régression majeure
  ou changement dangereux non validé.
- `COMMENTER` : observations utiles sans changement obligatoire.