---
description: Applique un incrément de refactoring Blazor validé : analyse, modification limitée, tests, build et rapport d'exécution.
allowed-tools:
  - read
  - search
  - glob
  - grep
  - edit
  - write
  - terminal
name: apply-blazor-refactoring
---

# Apply Refactoring

## Description
Applique un incrément de refactoring Blazor validé : analyse, modification limitée, tests, build et rapport d'exécution.

## When to use
- Invoquer pour : Applique un incrément de refactoring Blazor validé : analyse, modification limitée, tests, build et rapport d'exécution.
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
Applique un seul incrément de refactoring validé.

## Méthode

1. Lis l’incrément, les critères d’acceptation et les tests préalables.
2. Analyse les fichiers concernés.
3. Confirme que les tests de caractérisation existent.
4. Applique une seule responsabilité à la fois.
5. Exécute le build.
6. Exécute les tests pertinents.
7. Vérifie le formatage.
8. Produis un rapport d’exécution.

## Règles

- Refactore uniquement le périmètre fourni.
- Préserve strictement le comportement utilisateur observable.
- Ne modifie pas les contrats publics sans validation humaine.
- Ne modifie pas le schéma de base, les migrations ou l’authentification.
- Ne lance pas de migration ou de commande destructive.
- Ne supprime ou ne désactive jamais un test.
- Ne contourne pas une erreur de sécurité, d’autorisation ou de validation.
- Si un test échoue, analyse la cause racine ; ne masque pas l’échec.
- Si le refactoring dépasse le périmètre, arrête-toi et demande une clarification.

## Commandes autorisées

```bash
dotnet restore
dotnet build --no-restore
dotnet test 
dotnet test  --filter "FullyQualifiedName~<NomDuModule>"
dotnet format --verify-no-changes
```

## Sortie attendue

- Incrément appliqué
- Fichiers modifiés et créés
- Tests ajoutés ou mis à jour
- Résultats de build et tests
- Risques résiduels
- Décision : `INCRÉMENT APPLIQUÉ`, `INCRÉMENT PARTIEL`, `TESTS ÉCHOUÉS`

## Exemples
- Exemple : appliquer le skill à une cible nommée explicitement par l’appelant.
- Exemple : produire une sortie vérifiée avec chemins exacts et commandes exécutées.

## Limites
- Inspecter au plus 20 fichiers sans exploration exhaustive.
- Arrêter après 3 tentatives ciblées infructueuses.
- Consulter `../glossary.md` pour les termes communs.
