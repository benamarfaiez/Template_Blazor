---
description: Vérifie qu'un refactoring Blazor préserve le comportement utilisateur observable avant la création d'une PR.
allowed-tools:
  - read
  - search
  - glob
  - grep
  - terminal
  - playwright-mcp
name: verify-refactoring-behavior
---

# Verify Refactoring Behavior

## Description
Vérifie qu'un refactoring Blazor préserve le comportement utilisateur observable avant la création d'une PR.

## When to use
- Invoquer pour : Vérifie qu'un refactoring Blazor préserve le comportement utilisateur observable avant la création d'une PR.
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
Vérifie que le refactoring préserve le comportement utilisateur observable.

## Périmètre

Vérifie :

- Rendu des données principales
- Navigation
- Formulaires et validation
- Messages d’erreur
- États de chargement, vide et succès
- Interactions utilisateur
- Autorisations
- Appels API
- Parcours critiques

## Méthode

1. Identifie le comportement avant refactoring.
2. Identifie le comportement après refactoring.
3. Compare les tests de caractérisation.
4. Exécute les tests xUnit, bUnit et Playwright pertinents.
5. Vérifie les parcours critiques avec Playwright.
6. Signale toute différence de comportement.

## Règles

- Ne modifie pas le code de production.
- Ne supprime ou ne désactive jamais un test.
- Ne contourne pas une erreur de sécurité, d’autorisation ou de validation.
- Ne relance pas aveuglément une commande après un échec.
- Si une différence de comportement est détectée, arrête-toi et demande une
  clarification.

## Sortie attendue

- Comportement avant refactoring
- Comportement après refactoring
- Tests exécutés
- Différences détectées
- Risques résiduels
- Décision : `COMPORTEMENT PRÉSERVÉ` ou `RÉGRESSION DÉTECTÉE`

## Exemples
- Exemple : appliquer le skill à une cible nommée explicitement par l’appelant.
- Exemple : produire une sortie vérifiée avec chemins exacts et commandes exécutées.

## Limites
- Inspecter au plus 20 fichiers sans exploration exhaustive.
- Arrêter après 3 tentatives ciblées infructueuses.
- Consulter `../../common/glossary.md` pour les termes communs.
