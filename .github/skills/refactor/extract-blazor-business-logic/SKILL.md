---
description: Extrait la validation, les règles métier et les appels externes d'un composant Blazor Razor vers des services testables, sans changer le comportement utilisateur.
allowed-tools:
  - read
  - search
  - glob
  - grep
  - edit
  - write
  - terminal
name: extract-blazor-business-logic
---

# Extract Business Logic

## Description
Extrait la validation, les règles métier et les appels externes d'un composant Blazor Razor vers des services testables, sans changer le comportement utilisateur.

## When to use
- Invoquer pour : Extrait la validation, les règles métier et les appels externes d'un composant Blazor Razor vers des services testables, sans changer le comportement utilisateur.
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
Extrais la logique métier d’un composant Razor vers des services testables.
1. Identifie la validation, règle, calcul ou appel externe concerné.
2. Limite le périmètre aux 20 fichiers les plus liés.
3. Réutilise une interface ou crée une interface minimale.
4. Déplace la logique vers un service testable.
5. Injecte le service par constructeur ou `@inject`.
6. Conserve la présentation et l’état UI dans le composant.
7. Ajoute ou confirme les tests xUnit du service.
8. Ajoute ou confirme les tests bUnit du composant.
9. Exécute `dotnet build --no-restore` puis les tests ciblés.

## Exemples
- Exemple : appliquer le skill à une cible nommée explicitement par l’appelant.
- Exemple : produire une sortie vérifiée avec chemins exacts et commandes exécutées.

## Limites
- Inspecter au plus 20 fichiers sans exploration exhaustive.
- Arrêter après 3 tentatives ciblées infructueuses.
- Consulter `../../common/glossary.md` pour les termes communs.
