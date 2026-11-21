---
name: handle-merge-conflicts
description: "Résout des conflits Git dans un périmètre borné sans écraser ni modifier hors périmètre."
allowed-tools:
  - read
  - search
  - terminal
---

# Handle merge conflicts

## Description
Analyse puis résout les conflits entre deux changements compatibles.

## When to use
- `git status` reports unresolved conflict markers.
- A merge or rebase blocks the requested task.

## Inputs
- Conflicted file list.
- Base, ours and theirs intent where available.
- Maximum 3 resolution attempts.

## Outputs
- Resolved conflict list.
- Unresolved conflicts requiring human decision.
- Validation commands executed.

## Steps
1. List only conflicted files.
2. Read each conflict together with nearby unchanged code.
3. Preserve both intents when their behaviours remain compatible.
4. Remove conflict markers and run a targeted syntax check.
5. Stage each resolved file with `git add <file>` after removing its conflict markers and completing the syntax check.
6. Once all conflicts are resolved, continue the merge or rebase.
7. Stop after 3 failed resolutions and report the exact files.
8. Do not continue with destructive Git operations.

## Exemples
- Combine an import change with a renamed symbol.
- Preserve a test update while accepting an implementation change.

## Limites
- Never run `git reset --hard`, `git checkout --`, or `git clean`.
- Never discard one side without explicit user authorization.
