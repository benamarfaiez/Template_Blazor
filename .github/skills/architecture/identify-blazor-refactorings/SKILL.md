---
name: identify-blazor-refactorings
description: Identifie les opportunités de refactoring Blazor à faible risque à partir de la cartographie et des risques identifiés.
allowed-tools:
  - read
  - search
---

# Identifier les refactorings Blazor

Identifie les refactorings qui améliorent la maintenabilité, la testabilité,
la sécurité ou la performance sans changer le comportement utilisateur.

## Types de refactoring à rechercher

- Extraction de logique métier depuis un composant `.razor`
- Extraction de service ou de validateur
- Extraction de composant réutilisable
- Remplacement de duplication par un helper ou un composant partagé
- Introduction d’interface pour faciliter les tests
- Découpage d’un service à responsabilités multiples
- Remplacement d’une entité EF exposée à l’UI par un DTO
- Introduction de pagination ou de virtualisation
- Amélioration de la gestion d’erreurs et de l’annulation
- Modernisation C# sans changement de contrat public
- Renforcement des tests de caractérisation

## Critères de sélection

Propose uniquement un refactoring si :

- Le bénéfice est clair.
- Le risque de régression est maîtrisable.
- Le périmètre peut être livré dans une PR courte.
- Les tests nécessaires sont identifiables.
- Le changement est réversible.
- Il n’exige pas de décision métier ou d’architecture majeure non validée.

## Format de proposition

```md
### Refactoring : <titre>

**Problème :** constat ou risque concerné.

**Localisation :** `chemin/du/fichier:Lxx-Lyy`

**Bénéfice attendu :** maintenabilité, testabilité, sécurité ou performance.

**Périmètre proposé :** fichiers et modules concernés.

**Approche :** étapes techniques à réaliser.

**Risque :** faible / moyen / élevé.

**Tests préalables :** tests à ajouter ou confirmer.

**Effort estimé :** S / M / L.

**Dépendances :** autres refactorings nécessaires avant celui-ci.
```

## Règles

- Ne modifie aucun fichier.
- Ne propose pas de réécriture complète.
- Ne propose pas de changement de schéma de base, d’API publique ou
  d’authentification sans validation humaine.
- Ne propose pas plus de 10 refactorings.
- Chaque proposition doit être indépendante ou indiquer clairement ses dépendances.