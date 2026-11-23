---
description: Exécute les tests xUnit des services, validateurs, règles métier et mappers Blazor, puis analyse les résultats.
allowed-tools:
  - read
  - search
  - glob
  - terminal
name: qa-run-unit-tests
---

# Qa Run Unit Tests

## Description
Exécute les tests xUnit des services, validateurs, règles métier et mappers Blazor, puis analyse les résultats.

## When to use
- Invoquer pour : Exécute les tests xUnit des services, validateurs, règles métier et mappers Blazor, puis analyse les résultats.
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
Exécute les tests xUnit pertinents pour la cible ou la PR analysée.

## Commandes

```bash
dotnet test
dotnet test --filter "FullyQualifiedName~<NomDuModule>"
dotnet test --collect:"XPlat Code Coverage"
```

## Périmètre

Teste en priorité :

- Services d’application
- Validateurs
- Règles métier
- Mappers
- Handlers
- Helpers
- Logique de calcul, transformation ou décision

## Règles

- Exécute d’abord les tests ciblés, puis la suite complète si nécessaire.
- Ne relance pas aveuglément un test après un échec.
- Ne supprime ou ne désactive jamais un test.
- Ne modifie pas une assertion pour masquer un échec.
- Classe chaque échec : bug existant, régression ou problème de test.
- Pour un bug existant, signale-le et attends une décision humaine.

## Sortie attendue

- Commandes exécutées
- Tests réussis, échoués et ignorés
- Tests ciblés par la modification
- Couverture estimée ou mesurée
- Échecs et cause probable
- Tests prioritaires à ajouter

## Exemples
- Exemple : appliquer le skill à une cible nommée explicitement par l’appelant.
- Exemple : produire une sortie vérifiée avec chemins exacts et commandes exécutées.

## Limites
- Inspecter au plus 20 fichiers sans exploration exhaustive.
- Arrêter après 3 tentatives ciblées infructueuses.
- Consulter `../glossary.md` pour les termes communs.
