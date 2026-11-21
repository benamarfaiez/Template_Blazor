---
description: Exécute les tests bUnit des composants Blazor Razor et vérifie le rendu, les interactions, les états, la validation et les messages d'erreur.
allowed-tools:
  - read
  - search
  - glob
  - terminal
  - edit
name: qa-run-component-tests
---

# Qa Run Component Tests

## Description
Exécute les tests bUnit des composants Blazor Razor et vérifie le rendu, les interactions, les états, la validation et les messages d'erreur.

## When to use
- Invoquer pour : Exécute les tests bUnit des composants Blazor Razor et vérifie le rendu, les interactions, les états, la validation et les messages d'erreur.
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
Exécute les tests bUnit pertinents pour les composants Razor modifiés.

## Commandes

```bash
dotnet test --filter "FullyQualifiedName~<NomDuComposant>"
dotnet test 
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

## Exemples
- Exemple : appliquer le skill à une cible nommée explicitement par l’appelant.
- Exemple : produire une sortie vérifiée avec chemins exacts et commandes exécutées.

## Limites
- Inspecter au plus 20 fichiers sans exploration exhaustive.
- Arrêter après 3 tentatives ciblées infructueuses.
- Consulter `../../common/glossary.md` pour les termes communs.
