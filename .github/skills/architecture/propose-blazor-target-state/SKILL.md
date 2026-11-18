---
description: Propose une architecture cible progressive pour un projet Blazor, sans modifier le code ni imposer une réécriture complète.
allowed-tools:
  - read
  - search
name: propose-blazor-target-state
---

# Propose Target State

## Description
Propose une architecture cible progressive pour un projet Blazor, sans modifier le code ni imposer une réécriture complète.

## When to use
- Invoquer pour : Propose une architecture cible progressive pour un projet Blazor, sans modifier le code ni imposer une réécriture complète.
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
Propose une architecture cible progressive, adaptée au projet existant.

## Objectif

Décris l’état cible souhaitable à horizon court, moyen et long terme, sans
imposer une réécriture complète.

## Sections attendues

### 1. Architecture cible

Décris :

- Séparation UI / application / domaine / infrastructure
- Rôle des composants Razor
- Rôle des services d’application
- Rôle du domaine
- Rôle de l’infrastructure
- Gestion de l’état
- Gestion des erreurs
- Gestion de l’authentification et des autorisations

### 2. Principes directeurs

- Composants Razor centrés sur la présentation
- Logique métier dans des services testables
- Interfaces pour les dépendances externes
- DTO à la frontière des couches
- Tests de comportement plutôt que d’implémentation
- Sécurité et autorisation côté serveur
- Accessibilité intégrée dès la conception

### 3. Étapes de migration

Propose une migration progressive :

| Horizon | Objectif | Changements | Risque | Prérequis |
|---|---|---|---|---|
| Court terme | Stabiliser | Tests, extraction de services | Faible | Tests de caractérisation |
| Moyen terme | Découpler | Interfaces, DTO, composants réutilisables | Moyen | Couverture suffisante |
| Long terme | Standardiser | Architecture cible, conventions, observabilité | Moyen | Décisions validées |

### 4. Décisions à valider

Liste les décisions qui nécessitent une validation humaine :

- Choix d’un pattern d’état
- Introduction d’un médiateur ou d’un bus
- Migration d’API
- Modification du modèle de données
- Choix d’une bibliothèque
- Changement de stratégie d’authentification
- Refonte majeure d’un module

## Règles

- Ne modifie aucun fichier.
- Ne propose pas de big bang.
- Respecte les contraintes et l’existant.
- Indique clairement les compromis.
- Ne présente pas une recommandation comme une décision déjà prise.

## Exemples
- Exemple : appliquer le skill à une cible nommée explicitement par l’appelant.
- Exemple : produire une sortie vérifiée avec chemins exacts et commandes exécutées.

## Limites
- Inspecter au plus 20 fichiers sans exploration exhaustive.
- Arrêter après 3 tentatives ciblées infructueuses.
- Consulter `../../common/glossary.md` pour les termes communs.
