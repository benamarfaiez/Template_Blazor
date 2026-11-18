---
name: audit-blazor-performance
description: Audit en lecture seule de la performance, de la fiabilité, des requêtes EF Core, du rendu Blazor, de la gestion d'erreurs et de la résilience.
allowed-tools:
  - read
  - search
  - terminal
---

# Auditer la performance Blazor

Analyse la performance et la fiabilité sans modifier le projet.

## Périmètre

- Requêtes EF Core et accès aux données
- Appels HTTP et intégrations externes
- Rendu Blazor et gestion d’état
- Listes, pagination et virtualisation
- JavaScript interop
- SignalR et WebSockets
- Cache, mémoire et ressources
- Gestion des erreurs, timeouts, retries et cancellation
- Démarrage et taille du bundle, pour Blazor WebAssembly

## Constats à rechercher

### Accès aux données

- Requêtes N+1
- Chargement de colonnes ou d’entités inutiles
- Requêtes non paginées
- Absence de `AsNoTracking()` pour les lectures
- Requêtes dans une boucle
- Absence d’index ou de filtre approprié, à confirmer avec la base
- Entités EF exposées directement à l’UI

### Rendu Blazor

- Rendu excessif ou recalculs coûteux
- Listes longues sans `Virtualize`
- Absence de `@key` sur les listes dynamiques
- Appels à `StateHasChanged()` inutiles
- État global mutable ou mal partagé
- Traitement lourd dans le markup Razor

### Résilience

- Appels HTTP sans timeout
- Absence de `CancellationToken`
- Absence de gestion d’erreur
- Retry infini ou mal configuré
- Ressources non libérées
- `HttpClient` créé manuellement au lieu d’`IHttpClientFactory`
- Absence de circuit breaker pour les intégrations critiques
- Erreurs silencieuses ou messages techniques exposés à l’utilisateur

## Format de constat

```md
### [ÉLEVÉ | MOYEN | FAIBLE] Titre

**Localisation :** `chemin/du/fichier:Lxx-Lyy`

**Constat :** description factuelle.

**Impact :** performance, fiabilité, coût ou expérience utilisateur.

**Preuve :** extrait court du code.

**Action recommandée :** optimisation incrémentale.

**Mesure nécessaire :** métrique, benchmark ou test de charge à réaliser.

**Effort estimé :** S / M / L
```

## Règles

- Ne modifie aucun fichier.
- Ne propose pas d’optimisation sans preuve ou hypothèse clairement identifiée.
- Distingue un problème mesuré d’un risque potentiel.
- Recommande une mesure avant toute optimisation coûteuse.
- Ne lance pas de test de charge ou de commande destructive.