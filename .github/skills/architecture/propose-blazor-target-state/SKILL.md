---
name: propose-blazor-target-state
description: Propose une architecture cible progressive pour un projet Blazor, sans modifier le code ni imposer une réécriture complète.
allowed-tools:
  - read
  - search
---

# Proposer un état cible Blazor

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