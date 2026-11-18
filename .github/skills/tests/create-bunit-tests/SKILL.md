---
name: create-bunit-tests
description: Crée ou met à jour des tests bUnit pour les composants Blazor Razor, leurs paramètres, interactions, états, formulaires et messages d'erreur.
---

# Créer des tests bUnit

Ajoute ou renforce les tests bUnit pour verrouiller le comportement actuel des
composants Razor.

## Périmètre

Teste :

- Rendu initial
- Paramètres de composant
- Événements et interactions utilisateur
- Formulaires et validation
- États de chargement, vide, erreur et succès
- Navigation
- Appels aux services injectés
- Messages d’erreur et accessibilité de base

## Règles

- Place les tests dans le projet de tests de composants existant.
- Utilise des services factices ou mockés, pas une base de données réelle.
- Teste le rendu et les interactions visibles par l’utilisateur.
- Vérifie les états : chargement, vide, erreur et succès.
- Vérifie les messages d’erreur et la validation.
- Vérifie les labels, rôles et textes accessibles lorsque pertinent.
- Utilise `EventCallback<T>` et les paramètres du composant tels qu’ils existent.
- Ne modifie pas le composant Razor.
- Ne supprime ni ne désactive un test existant.

## Couverture minimale

Pour chaque composant, couvre au minimum :

- Rendu des données principales
- Interaction principale
- État de chargement
- État vide
- État d’erreur
- Validation ou message d’erreur, si applicable