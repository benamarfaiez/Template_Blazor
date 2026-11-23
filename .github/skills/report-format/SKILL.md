---
name: report-format
description: "Produit un rapport structuré, vérifiable et limité aux résultats pertinents."
---

# Report format

## Description
Standardise les constats, preuves et décisions finales.

## When to use
- Return audit, QA, review or refactoring findings.
- Summarise build or test outcomes.

## Inputs
- Findings with exact file paths.
- Verification evidence.
- Maximum 20 findings per report. If more verified findings exist, continue in additional reports or attach a complete findings list.

## Outputs
- Summary table or findings list.
- Residual risks.
- Final decision.
- Missing information.

## Steps
1. Separate verified facts from hypotheses.
2. Cite exact paths and line numbers when available.
3. Rank findings by severity or impact.
4. Include commands and outcomes for every verification claim.
5. End with one explicit decision or blocked status.
6. Limit each report to 20 findings. If more verified findings exist, continue in additional reports or attach a complete findings list; do not omit or hide any verified failures.

## Exemples
- QA report: validated, validated with reservations, or failed.
- Refactoring report: ready, partial or blocked.

## Limites
- Do not invent evidence or missing results.
- Do not hide failures or uncertainties.
