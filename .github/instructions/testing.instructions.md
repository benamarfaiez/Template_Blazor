---
applyTo: "**/*Tests*/**/*.cs"
---

# Règles de tests

## Principes

- Teste le comportement observable, pas les détails d’implémentation.
- Un test doit être indépendant, reproductible et rapide.
- Nomme les tests selon le scénario et le résultat attendu.
- Utilise la convention `Méthode_Scénario_RésultatAttendu` si le projet ne définit pas déjà une convention.

## Tests unitaires

- Utilise xUnit.
- Utilise Moq ou NSubstitute pour les dépendances externes.
- Teste les cas nominaux, les erreurs métier, les entrées invalides et les cas limites.
- Ne mocke pas la logique métier elle-même.
- Évite les tests qui dépendent de l’horloge, du réseau, du système de fichiers ou de l’ordre d’exécution.

## Tests Blazor

- Utilise bUnit pour les composants Razor.
- Teste le rendu, les interactions, la validation, les états de chargement et les erreurs.
- Utilise des services factices ou mockés plutôt qu’une base de données réelle.
- Vérifie les parcours accessibles : labels, messages d’erreur et états de focus.

## Tests E2E

- Utilise Playwright pour les parcours utilisateurs critiques.
- Teste connexion, création, modification, suppression, recherche, navigation et gestion d’erreurs.
- Utilise des données de test isolées et reproductibles.
- Ne rends pas la suite E2E dépendante d’un environnement externe non contrôlé.

## Refactoring

- Avant un refactoring, écris ou confirme les tests de caractérisation.
- Ne supprime pas un test sans justification explicite et validation humaine.
- Un test qui échoue doit conduire à une correction du code ou à une mise à jour justifiée du test.