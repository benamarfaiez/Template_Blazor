---
name: analyze-blazor-target
description: Analyse une cible Blazor et identifie le comportement observable, les dépendances, les tests existants et les risques de régression avant un refactoring.
---

# Analyser une cible Blazor

Analyse la cible fournie sans modifier le code.

## Étapes

1. Identifie le type de cible : composant Razor, service, validateur, handler, repository ou parcours utilisateur.
2. Lis les fichiers concernés, les tests existants et la documentation pertinente.
3. Identifie le comportement observable à préserver.
4. Identifie les dépendances externes : base de données, HTTP, cache, files, authentification, JavaScript interop et SignalR.
5. Identifie les tests existants et leurs lacunes.
6. Liste les risques de régression.
7. Recommande les types de tests nécessaires.

## Sortie attendue

Produis :

- Cible analysée et fichiers concernés
- Comportement observable à préserver
- Dépendances et intégrations
- Tests existants
- Lacunes de couverture
- Risques de régression
- Plan de tests recommandé : xUnit, bUnit et/ou Playwright
- Questions à clarifier, si nécessaire

## Règles

- Ne modifie aucun fichier.
- Ne présente pas une hypothèse comme un fait.
- Cite les fichiers et, si possible, les lignes concernées.
- Ne recommande pas de refactoring : ce skill prépare uniquement les tests.