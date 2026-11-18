---
name: verify-refactoring-behavior
description: Vérifie qu'un refactoring Blazor préserve le comportement utilisateur observable avant la création d'une PR.
allowed-tools:
  - read
  - search
  - glob
  - grep
  - terminal
  - playwright-mcp
---

# Vérifier le comportement après refactoring

Vérifie que le refactoring préserve le comportement utilisateur observable.

## Périmètre

Vérifie :

- Rendu des données principales
- Navigation
- Formulaires et validation
- Messages d’erreur
- États de chargement, vide et succès
- Interactions utilisateur
- Autorisations
- Appels API
- Parcours critiques

## Méthode

1. Identifie le comportement avant refactoring.
2. Identifie le comportement après refactoring.
3. Compare les tests de caractérisation.
4. Exécute les tests xUnit, bUnit et Playwright pertinents.
5. Vérifie les parcours critiques avec Playwright.
6. Signale toute différence de comportement.

## Règles

- Ne modifie pas le code de production.
- Ne supprime ou ne désactive jamais un test.
- Ne contourne pas une erreur de sécurité, d’autorisation ou de validation.
- Ne relance pas aveuglément une commande après un échec.
- Si une différence de comportement est détectée, arrête-toi et demande une
  clarification.

## Sortie attendue

- Comportement avant refactoring
- Comportement après refactoring
- Tests exécutés
- Différences détectées
- Risques résiduels
- Décision : `COMPORTEMENT PRÉSERVÉ` ou `RÉGRESSION DÉTECTÉE`