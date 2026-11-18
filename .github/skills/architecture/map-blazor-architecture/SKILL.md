---
name: map-blazor-architecture
description: Cartographie les pages, composants, services, modèles, dépendances et flux de données d'un projet Blazor sans modifier le code.
allowed-tools:
  - read
  - search
---

# Cartographier l’architecture Blazor

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