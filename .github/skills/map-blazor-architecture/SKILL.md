---
description: Cartographie les pages, composants, services, modèles, dépendances et flux de données d'un projet Blazor sans modifier le code.
allowed-tools:
  - read
  - search
name: map-blazor-architecture
---

# Map Architecture

## Description
Cartographie les pages, composants, services, modèles, dépendances et flux de données d'un projet Blazor sans modifier le code.

## When to use
- Invoquer pour : Cartographie les pages, composants, services, modèles, dépendances et flux de données d'un projet Blazor sans modifier le code.
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
Produis une cartographie factuelle de l’architecture du projet.

## Éléments à cartographier

- Pages et routes
- Layouts
- Composants réutilisables
- Services et interfaces
- Validateurs
- Repositories et accès aux données
- DTO, entités et modèles de domaine
- Configuration DI
- Endpoints API
- Flux de données entre UI, services et infrastructure

## Tableau de cartographie

| Zone | Fichiers clés | Responsabilité | Dépendances | Risque |
|---|---|---|---|---|
| Pages |  |  |  | Faible / Moyen / Élevé |
| Composants |  |  |  |  |
| Services |  |  |  |  |
| Domaine |  |  |  |  |
| Infrastructure |  |  |  |  |
| Tests |  |  |  |  |

## Analyse à produire

- Composants volumineux ou à responsabilités multiples
- Logique métier présente dans les composants `.razor`
- Duplication de code
- Couplage UI / accès aux données / règles métier
- Dépendances circulaires
- Services avec trop de responsabilités
- Absence d’interfaces ou de séparation des responsabilités
- Zones sans tests

## Règles

- Ne modifie aucun fichier.
- Base chaque constat sur une preuve de code.
- Indique le chemin et, si possible, les lignes concernées.
- Classe chaque zone par risque : `Faible`, `Moyen` ou `Élevé`.

## Exemples
- Exemple : appliquer le skill à une cible nommée explicitement par l’appelant.
- Exemple : produire une sortie vérifiée avec chemins exacts et commandes exécutées.

## Limites
- Inspecter au plus 20 fichiers sans exploration exhaustive.
- Arrêter après 3 tentatives ciblées infructueuses.
- Consulter `../glossary.md` pour les termes communs.
