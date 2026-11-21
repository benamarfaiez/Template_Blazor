---
description: Analyse le périmètre d'une pull request Blazor : objectif, fichiers modifiés, impact, risques et critères d'acceptation.
allowed-tools:
  - read
  - search
  - github-mcp/get_pull_request
  - github-mcp/get_pull_request_diff
name: code-review-blazor-scope
---

# Code Review Scope

## Description
Analyse le périmètre d'une pull request Blazor : objectif, fichiers modifiés, impact, risques et critères d'acceptation.

## When to use
- Invoquer pour : Analyse le périmètre d'une pull request Blazor : objectif, fichiers modifiés, impact, risques et critères d'acceptation.
- Utiliser uniquement lorsque la cible correspond au périmètre du skill.

## Inputs
- Cible, PR, issue ou chemins fournis par l’appelant.
- Contraintes humaines explicites et état Git vérifié.
- Périmètre limité à 50 fichiers au premier passage; pour une PR dépassant 50 fichiers modifiés, poursuivre la revue par lots successifs jusqu'à couvrir tous les fichiers.

## Outputs
- Résultat vérifié correspondant à la mission du skill.
- Chemins exacts, preuves et décisions humaines restantes.
- Informations inconnues signalées par `À confirmer`.

## Steps
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

## Exemples
- Exemple : appliquer le skill à une cible nommée explicitement par l’appelant.
- Exemple : produire une sortie vérifiée avec chemins exacts et commandes exécutées.

## Limites
- Inspecter au plus 20 fichiers par lot. Si tous les fichiers ne peuvent pas être couverts, signaler explicitement la couverture incomplète, les fichiers non examinés et ne pas approuver sur la base d'une revue incomplète.
- Arrêter après 3 tentatives ciblées infructueuses.
- Consulter `../../common/glossary.md` pour les termes communs.
