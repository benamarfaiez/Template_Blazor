---
description: Crée ou met à jour des tests bUnit pour les composants Blazor Razor, leurs paramètres, interactions, états, formulaires et messages d'erreur.
name: create-bunit-tests
---

# Create Bunit Tests

## Description
Crée ou met à jour des tests bUnit pour les composants Blazor Razor, leurs paramètres, interactions, états, formulaires et messages d'erreur.

## When to use
- Invoquer pour : Crée ou met à jour des tests bUnit pour les composants Blazor Razor, leurs paramètres, interactions, états, formulaires et messages d'erreur.
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

## Exemples
- Exemple : appliquer le skill à une cible nommée explicitement par l’appelant.
- Exemple : produire une sortie vérifiée avec chemins exacts et commandes exécutées.

## Limites
- Inspecter au plus 20 fichiers sans exploration exhaustive.
- Arrêter après 3 tentatives ciblées infructueuses.
- Consulter `../glossary.md` pour les termes communs.
