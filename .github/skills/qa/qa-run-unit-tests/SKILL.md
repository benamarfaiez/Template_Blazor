---
name: qa-run-unit-tests
description: Exécute les tests xUnit des services, validateurs, règles métier et mappers Blazor, puis analyse les résultats.
allowed-tools:
  - read
  - search
  - glob
  - terminal
---

# Exécuter les tests unitaires

Exécute les tests xUnit pertinents pour la cible ou la PR analysée.

## Commandes

```bash
dotnet test --no-build
dotnet test --no-build --filter "FullyQualifiedName~<NomDuModule>"
dotnet test --collect:"XPlat Code Coverage"
```

## Périmètre

Teste en priorité :

- Services d’application
- Validateurs
- Règles métier
- Mappers
- Handlers
- Helpers
- Logique de calcul, transformation ou décision

## Règles

- Exécute d’abord les tests ciblés, puis la suite complète si nécessaire.
- Ne relance pas aveuglément un test après un échec.
- Ne supprime ou ne désactive jamais un test.
- Ne modifie pas une assertion pour masquer un échec.
- Classe chaque échec : bug existant, régression ou problème de test.
- Pour un bug existant, signale-le et attends une décision humaine.

## Sortie attendue

- Commandes exécutées
- Tests réussis, échoués et ignorés
- Tests ciblés par la modification
- Couverture estimée ou mesurée
- Échecs et cause probable
- Tests prioritaires à ajouter