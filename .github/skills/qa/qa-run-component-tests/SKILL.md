---
name: qa-run-component-tests
description: Exécute les tests bUnit des composants Blazor Razor et vérifie le rendu, les interactions, les états, la validation et les messages d'erreur.
allowed-tools:
  - read
  - search
  - glob
  - terminal
  - edit
---

# Exécuter les tests bUnit

Exécute les tests bUnit pertinents pour les composants Razor modifiés.

## Commandes

```bash
dotnet test --no-build --filter "FullyQualifiedName~<NomDuComposant>"
dotnet test --no-build
```

## Périmètre

Vérifie :

- Rendu initial du composant
- Paramètres et événements
- Interactions utilisateur
- Formulaires et validation
- États de chargement, vide, erreur et succès
- Navigation
- Messages d’erreur
- Accessibilité de base

## Règles

- Utilise des services factices ou mockés, pas une base de données réelle.
- Teste le comportement observable, pas les détails d’implémentation.
- Ne modifie pas le composant Razor pour faire passer un test.
- Ne supprime ou ne désactive jamais un test.
- Ajoute un test seulement s’il couvre une régression ou un comportement critique.
- Pour un échec, distingue bug, régression et problème de test.

## Couverture minimale

Pour chaque composant modifié, vérifie :

- Rendu des données principales
- Interaction principale
- État de chargement
- État vide
- État d’erreur
- Validation ou message d’erreur, si applicable