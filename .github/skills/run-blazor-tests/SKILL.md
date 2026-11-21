---
description: Exécute les tests xUnit, bUnit et Playwright pertinents, analyse les résultats et distingue les échecs de test des bugs existants.
name: run-blazor-tests
---

# Run Tests

## Description
Exécute les tests xUnit, bUnit et Playwright pertinents, analyse les résultats et distingue les échecs de test des bugs existants.

## When to use
- Invoquer pour : Exécute les tests xUnit, bUnit et Playwright pertinents, analyse les résultats et distingue les échecs de test des bugs existants.
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
Exécute uniquement les tests pertinents pour la cible analysée.

## Commandes autorisées

```bash
dotnet build --no-restore
dotnet test 
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

## Exemples
- Exemple : appliquer le skill à une cible nommée explicitement par l’appelant.
- Exemple : produire une sortie vérifiée avec chemins exacts et commandes exécutées.

## Limites
- Inspecter au plus 20 fichiers sans exploration exhaustive.
- Arrêter après 3 tentatives ciblées infructueuses.
- Consulter `../glossary.md` pour les termes communs.
