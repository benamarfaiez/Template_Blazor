---
name: define-refactoring-acceptance-criteria
description: Définit les critères d'acceptation, les tests préalables, les risques, les dépendances et le rollback pour chaque incrément de refactoring Blazor.
allowed-tools:
  - read
  - search
  - glob
  - grep
---

# Définir les critères d’acceptation

Pour chaque incrément de refactoring, définis des critères clairs, mesurables
et vérifiables.

## Éléments à définir

### 1. Comportement à préserver

Décris le comportement observable qui ne doit pas changer :

- Rendu de l’interface
- Navigation
- Validation
- Messages d’erreur
- États de chargement, vide et succès
- Appels API
- Autorisations
- Données affichées

### 2. Objectif du refactoring

Indique précisément :

- Problème résolu
- Bénéfice attendu
- Responsabilité extraite ou déplacée
- Composants, services et tests concernés

### 3. Périmètre technique

Liste :

- Fichiers à modifier
- Fichiers à créer
- Fichiers à ne pas modifier
- Dépendances internes et externes
- Contrats publics à préserver

### 4. Tests préalables

Indique les tests à ajouter ou confirmer avant modification :

- Tests xUnit pour les services et règles métier
- Tests bUnit pour les composants Razor
- Tests Playwright pour les parcours critiques
- Tests de régression à protéger

### 5. Critères d’acceptation

Utilise des critères vérifiables :

```md
- [ ] Le build `dotnet build --no-restore` réussit.
- [ ] Les tests xUnit, bUnit et Playwright pertinents réussissent.
- [ ] Le comportement utilisateur existant est préservé.
- [ ] La logique métier est extraite du composant Razor.
- [ ] Aucun contrat public n’est modifié.
- [ ] Aucun secret n’est introduit.
- [ ] La couverture des règles métier modifiées est suffisante.
- [ ] La PR est limitée au périmètre défini.
```

### 6. Risques et mitigations

Pour chaque risque :

| Risque | Impact | Mitigation | Test de validation |
|---|---|---|---|
| Régression de validation | Formulaire incorrect | Tests bUnit de validation | Test de soumission invalide |
| Modification de contrat public | Rupture d’API | Aucun changement de contrat | Tests d’intégration |
| Régression de rendu | UI incorrecte | Tests bUnit de rendu | Test de rendu initial |

### 7. Dépendances

Indique :

- Incréments à réaliser avant
- Incréments réalisables en parallèle
- Incréments bloqués par une décision humaine
- Migrations ou changements d’infrastructure nécessaires

### 8. Rollback

Définis une procédure simple :

- Revert de la PR
- Restauration d’un fichier ou d’une configuration
- Rollback de migration, uniquement si une migration a été validée
- Critère de détection d’un problème après fusion

## Règles

- Ne modifie aucun fichier.
- Ne crée pas d’issue.
- Ne crée pas de PR.
- Ne propose pas de réécriture complète.
- Ne mélange pas plusieurs responsabilités dans un incrément.
- Un critère doit être vérifiable par un test, une commande ou une observation claire.