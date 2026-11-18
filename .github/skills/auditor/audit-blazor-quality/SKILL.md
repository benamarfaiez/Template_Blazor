---
name: audit-blazor-quality
description: Audit en lecture seule de la qualité, de la maintenabilité, de la testabilité, de l'accessibilité et de la robustesse d'un projet Blazor.
allowed-tools:
  - read
  - search
  - terminal
---

# Auditer la qualité Blazor

Analyse la qualité du code Blazor et .NET sans modifier le projet.

## Périmètre

- Composants `.razor` et `.razor.cs`
- Services, interfaces et classes métier
- DTO, entités et modèles
- Gestion des erreurs
- Tests unitaires, bUnit et Playwright
- Accessibilité des composants
- Documentation et conventions du dépôt

## Constats à rechercher

### Composants Razor

- Composant de plus de 300 lignes
- Plus de cinq responsabilités dans un composant
- Logique métier dans le markup Razor
- Duplication entre composants
- Paramètres mutés
- `StateHasChanged()` excessif ou mal placé
- Gestion incorrecte du cycle de vie
- Ressources non libérées
- Absence de `CancellationToken`
- Rendu coûteux ou listes non virtualisées

### Code C#

- Méthodes trop longues ou trop complexes
- Classes avec trop de responsabilités
- Duplication significative
- Dépendances circulaires
- Couplage fort entre couches
- Absence d’interfaces
- Exceptions avalées sans journalisation
- `async void`, `.Result`, `.Wait()` ou `.GetAwaiter().GetResult()`
- Utilisation directe de `DateTime.Now`, `Guid.NewGuid()` ou `Random`
  dans la logique métier

### Tests

- Absence de tests sur une règle métier critique
- Tests dépendants de l’ordre, du réseau, de l’horloge ou d’une base partagée
- Tests qui valident l’implémentation plutôt que le comportement
- Absence de tests bUnit pour les composants interactifs
- Absence de tests Playwright pour les parcours critiques
- Couverture faible sur les zones à risque

### Accessibilité

- Champs sans label accessible
- Navigation clavier incomplète
- Focus non visible
- Messages d’erreur peu clairs
- Contraste insuffisant
- Composants interactifs non accessibles
- États de chargement ou d’erreur non annoncés

## Format de constat

```md
### [SÉVÉRITÉ] Titre

**Localisation :** `chemin/du/fichier:Lxx-Lyy`

**Constat :** description factuelle.

**Impact :** maintenabilité, testabilité, robustesse ou UX.

**Preuve :** extrait court du code.

**Action recommandée :** correction incrémentale.

**Effort estimé :** S / M / L

**Risque de changement :** Faible / Moyen / Élevé

**Tests nécessaires :** tests à ajouter ou mettre à jour.
```

## Niveaux de sévérité

- `BLOQUANT` : bug critique, perte de données ou régression majeure
- `ÉLEVÉ` : dette forte, testabilité insuffisante ou risque de production
- `MOYEN` : maintenabilité, duplication ou UX à améliorer
- `FAIBLE` : style, nommage ou documentation