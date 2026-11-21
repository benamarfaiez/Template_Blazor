---
name: read-project-structure
description: "Lit une structure de projet bornée pour identifier les fichiers, projets, tests et commandes pertinents."
allowed-tools:
  - read
  - search
---

# Read project structure

## Description
Inspecte uniquement les fichiers nécessaires pour cartographier un dépôt.

## When to use
- Start a task with no verified repository map.
- Identify the projects and validation commands of a .NET solution.

## Inputs
- Repository root.
- One task scope or target path.
- Maximum 20 files to inspect in the initial pass.

## Outputs
- Verified project list.
- Relevant source, test and configuration paths.
- Missing information explicitly labelled `À confirmer`.

## Steps
1. Read `README.md`, `*.sln`, `*.csproj`, `global.json` and test project manifests.
2. Limit the initial scan to paths matching the task scope.
3. Record exact paths and measured evidence.
4. Stop after 20 files or when the required project map is complete.
5. Report missing files instead of guessing.

## Exemples
- Identify `MonProjet.Client.csproj`, `MonProjet.Server.csproj` and test projects.
- Map a named feature from its page, service and test paths.

## Limites
- Do not scan the whole repository.
- Do not infer an SDK, framework or command without project evidence.
