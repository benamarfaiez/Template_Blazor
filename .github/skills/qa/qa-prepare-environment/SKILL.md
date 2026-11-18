---
description: Prépare l'environnement de QA Blazor : vérifie le SDK .NET, restaure les dépendances si nécessaire, identifie les projets de tests et vérifie l'état Git.
allowed-tools:
  - read
  - search
  - glob
  - terminal
name: qa-prepare-environment
---

# Qa Prepare Environment

## Description
Prépare l'environnement de QA Blazor : vérifie le SDK .NET, restaure les dépendances si nécessaire, identifie les projets de tests et vérifie l'état Git.

## When to use
- Invoquer pour : Prépare l'environnement de QA Blazor : vérifie le SDK .NET, restaure les dépendances si nécessaire, identifie les projets de tests et vérifie l'état Git.
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
Prépare l’environnement sans modifier le code de production.

## Étapes

1. Vérifie la présence du SDK .NET.
2. Identifie la solution, les projets et les projets de tests.
3. Détecte la variante Blazor : Server, WebAssembly, Auto ou Hybrid.
4. Vérifie l’état Git : branche, fichiers modifiés et fichiers indexés.
5. Restaure les dépendances uniquement si cela est nécessaire et autorisé.
6. Identifie les commandes de build et de tests configurées dans le dépôt.
7. Vérifie qu’aucun secret n’est exposé dans les fichiers modifiés.

## Commandes autorisées

```bash
dotnet --version
dotnet restore
dotnet build --no-restore
dotnet test --no-build
dotnet list package --vulnerable --include-transitive
```

## Règles

- Ne lance pas de migration de base de données.
- Ne lance pas de commande destructive.
- Ne modifie pas les fichiers de configuration de production.
- Ne stocke pas de secret dans un log ou une variable d’environnement persistante.
- Si l’environnement est incomplet, demande une clarification plutôt que de deviner.

## Exemples
- Exemple : appliquer le skill à une cible nommée explicitement par l’appelant.
- Exemple : produire une sortie vérifiée avec chemins exacts et commandes exécutées.

## Limites
- Inspecter au plus 20 fichiers sans exploration exhaustive.
- Arrêter après 3 tentatives ciblées infructueuses.
- Consulter `../../common/glossary.md` pour les termes communs.
