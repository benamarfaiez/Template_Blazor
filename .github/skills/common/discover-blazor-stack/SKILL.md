---
description: Identifie la stack .NET et Blazor, les projets, les dépendances, les commandes de build/test et les contraintes de déploiement d'un dépôt.
allowed-tools:
  - read
  - search
  - terminal
name: discover-blazor-stack
---

# Discover Stack

## Description
Identifie la stack .NET et Blazor, les projets, les dépendances, les commandes de build/test et les contraintes de déploiement d'un dépôt.

## When to use
- Invoquer pour : Identifie la stack .NET et Blazor, les projets, les dépendances, les commandes de build/test et les contraintes de déploiement d'un dépôt.
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
Identifie la stack technique du dépôt sans modifier aucun fichier.

## Fichiers à analyser

- `README.md`
- `AGENTS.md`
- `.github/copilot-instructions.md`
- `*.sln`
- `*.csproj`
- `Directory.Build.props`
- `Directory.Packages.props`
- `global.json`
- `nuget.config`
- `appsettings*.json`
- `Dockerfile`
- `docker-compose*.yml`
- Fichiers CI/CD, sans les modifier

## Informations à produire

| Élément | Valeur détectée | Preuve |
|---|---|---|
| Version .NET |  | `TargetFramework` |
| Variante Blazor | Server / WebAssembly / Auto / Hybrid | Références de packages et configuration |
| Projets |  | Fichiers `.csproj` |
| Packages clés |  | Références de packages |
| Base de données |  | Configuration et providers |
| ORM |  | Packages et code |
| Authentification |  | Configuration et packages |
| Tests | xUnit / bUnit / Playwright | Projets de tests |
| Build |  | Commandes détectées |
| Tests |  | Commandes détectées |
| Déploiement |  | CI/CD, Docker, manifestes |

## Règles

- Ne modifie aucun fichier.
- Ne lance aucune commande destructive.
- Si une information est incertaine, indique `hypothèse à confirmer`.
- Ne divulgue aucun secret.
- Signale les dépendances vulnérables ou obsolètes détectées.

## Exemples
- Exemple : appliquer le skill à une cible nommée explicitement par l’appelant.
- Exemple : produire une sortie vérifiée avec chemins exacts et commandes exécutées.

## Limites
- Inspecter au plus 20 fichiers sans exploration exhaustive.
- Arrêter après 3 tentatives ciblées infructueuses.
- Consulter `../common/glossary.md` pour les termes communs.
