---
description: Analyse une cible Blazor et identifie le comportement observable, les dépendances, les tests existants et les risques de régression avant un refactoring.
name: analyze-blazor-target
---

# Analyze Target

## Description
Analyse une cible Blazor et identifie le comportement observable, les dépendances, les tests existants et les risques de régression avant un refactoring.

## When to use
- Invoquer pour : Analyse une cible Blazor et identifie le comportement observable, les dépendances, les tests existants et les risques de régression avant un refactoring.
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

## Exemples
- Exemple : appliquer le skill à une cible nommée explicitement par l’appelant.
- Exemple : produire une sortie vérifiée avec chemins exacts et commandes exécutées.

## Limites
- Inspecter au plus 20 fichiers sans exploration exhaustive.
- Arrêter après 3 tentatives ciblées infructueuses.
- Consulter `../../common/glossary.md` pour les termes communs.
