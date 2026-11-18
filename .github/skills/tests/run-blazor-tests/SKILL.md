---
name: run-blazor-tests
description: Exécute les tests xUnit, bUnit et Playwright pertinents, analyse les résultats et distingue les échecs de test des bugs existants.
---

# Exécuter les tests Blazor

Exécute uniquement les tests pertinents pour la cible analysée.

## Commandes autorisées

```bash
dotnet build --no-restore
dotnet test --no-build
dotnet test --collect:"XPlat Code Coverage"
dotnet test --filter "FullyQualifiedName~<NomDuModule>"
```

Pour Playwright, utilise la commande déjà configurée dans le dépôt.

## Méthode

1. Exécute le build si nécessaire.
2. Exécute les tests unitaires ciblés.
3. Exécute les tests bUnit ciblés.
4. Exécute les tests Playwright ciblés, si la cible le justifie.
5. Analyse les échecs.
6. Classe chaque échec :
   - `ÉCHEC DE TEST` : problème de test, mock, fixture ou environnement.
   - `BUG EXISTANT` : le test révèle un comportement incorrect du code.
   - `RÉGRESSION` : le test échoue après une modification du code de production.
7. Corrige uniquement les problèmes de test, de fixture ou de configuration de test.
8. Ne corrige pas un bug de production sans validation explicite.

## Règles

- N’exécute jamais une commande destructive.
- Ne supprime ni ne désactive un test.
- Ne modifie pas une assertion pour masquer un échec.
- Ne contourne pas une erreur de sécurité, d’autorisation ou de validation.
- Si un test échoue, indique la cause, la preuve et l’action recommandée.