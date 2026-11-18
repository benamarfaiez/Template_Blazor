---
name: code-review-blazor-scope
description: Analyse le périmètre d'une pull request Blazor : objectif, fichiers modifiés, impact, risques et critères d'acceptation.
allowed-tools:
  - read
  - search
  - github-mcp/get_pull_request
  - github-mcp/get_pull_request_diff
---

# Définir le périmètre de revue

Analyse la PR sans modifier le code.

## Informations à extraire

- Titre, description et objectif de la PR
- Issues, tickets ou incidents liés
- Fichiers modifiés
- Composants, services, tests et configuration concernés
- Changements de comportement utilisateur
- Changements de contrat d’API
- Changements de schéma de base ou migration
- Changements de dépendance
- Changements de CI/CD ou de configuration de déploiement

## Questions à vérifier

- La PR a-t-elle un objectif unique et clair ?
- Le périmètre est-il limité et cohérent ?
- Le comportement utilisateur est-il préservé, sauf changement explicitement demandé ?
- Les tests couvrent-ils le changement ?
- Y a-t-il des fichiers modifiés hors périmètre ?
- Y a-t-il des secrets, fichiers générés ou modifications dangereuses ?
- La description de PR explique-t-elle les risques et le rollback ?

## Sortie attendue

Produis :

- Résumé de la PR
- Périmètre technique
- Impact utilisateur
- Risques principaux
- Critères d’acceptation
- Questions à clarifier
- Liste des skills de revue à appliquer