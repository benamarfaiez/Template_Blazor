---
name: check-vulnerable-dependencies
description: "Vérifie les dépendances NuGet avec une portée et des commandes limitées."
allowed-tools:
  - read
  - search
  - terminal
---

# Check vulnerable dependencies

## Description
Identifie les dépendances concernées et les versions disponibles après audit.

## When to use
- Review a NuGet manifest or package vulnerability.
- Produce a dependency remediation report.

## Inputs
- One solution or one project manifest.
- Package source configured by the repository.
- Maximum 10 vulnerabilities to detail per report.

## Outputs
- Affected project and package.
- Current and recommended versions.
- Verification status and residual uncertainty.

## Steps
1. Run `dotnet list <solution-or-project> package --vulnerable --include-transitive`.
2. Limit findings to the project supplied by the task.
3. Verify package availability from the configured package source.
4. Report up to 10 highest-confidence findings.
5. Do not upgrade or edit a manifest without explicit scope.

## Exemples
- Report a direct package vulnerability affecting one .NET project.
- Flag an unresolved transitive dependency for manual validation.

## Limites
- Do not modify package manifests.
- Do not claim a fix when verification has not succeeded.
