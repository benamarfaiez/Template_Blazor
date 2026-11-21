---
description: Modernise le code C# Blazor vers les idiomes .NET récents sans changer les contrats publics, le comportement utilisateur ou les dépendances majeures.
allowed-tools:
  - read
  - search
  - glob
  - grep
  - edit
  - write
  - terminal
name: modernize-blazor-csharp
---

# Modernize Csharp

## Description
Modernise le code C# Blazor vers les idiomes .NET récents sans changer les contrats publics, le comportement utilisateur ou les dépendances majeures.

## When to use
- Invoquer pour : Modernise le code C# Blazor vers les idiomes .NET récents sans changer les contrats publics, le comportement utilisateur ou les dépendances majeures.
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
Modernise le code C# sans changer le comportement utilisateur ni les contrats
publics.

## Modernisations autorisées

- Remplacement de types par `record` lorsque le type est immuable.
- Utilisation de `required` pour les propriétés obligatoires.
- Utilisation de pattern matching.
- Utilisation de collection expressions.
- Utilisation de `TimeProvider` au lieu de `DateTime.Now`.
- Utilisation de `CancellationToken` dans les opérations asynchrones longues.
- Remplacement de code dupliqué par des helpers ou méthodes réutilisables.
- Amélioration du nommage et de la lisibilité.

## Modernisations interdites

- Modification d’un contrat public.
- Modification du schéma de base.
- Modification de l’authentification ou des autorisations.
- Ajout ou suppression d’une dépendance majeure.
- Migration de framework.
- Changement de comportement utilisateur.
- Suppression ou désactivation de test.
- Contournement d’une règle de sécurité.

## Méthode

1. Identifie le code à moderniser.
2. Vérifie la compatibilité avec la version .NET du projet.
3. Applique une modernisation à la fois.
4. Ajoute ou confirme les tests nécessaires.
5. Exécute le build et les tests.
6. Vérifie le formatage avec `dotnet format --verify-no-changes`.

## Règles

- Préserve strictement le comportement utilisateur observable.
- Respecte `.editorconfig` et les analyzers existants.
- Utilise l’immuabilité lorsque possible.
- Utilise l’injection par constructeur.
- Évite `async void`, `.Result`, `.Wait()` et `.GetAwaiter().GetResult()`.
- Ne lance pas de migration ou de commande destructive.

## Exemples
- Exemple : appliquer le skill à une cible nommée explicitement par l’appelant.
- Exemple : produire une sortie vérifiée avec chemins exacts et commandes exécutées.

## Limites
- Inspecter au plus 20 fichiers sans exploration exhaustive.
- Arrêter après 3 tentatives ciblées infructueuses.
- Consulter `../glossary.md` pour les termes communs.
