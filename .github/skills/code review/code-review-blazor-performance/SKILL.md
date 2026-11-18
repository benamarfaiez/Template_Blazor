---
description: Vérifie la performance, la fiabilité, les requêtes EF Core, le rendu Blazor, la résilience et la gestion des ressources dans une pull request.
allowed-tools:
  - read
  - search
  - github-mcp/get_pull_request_diff
name: code-review-blazor-performance
---

# Code Review Performance

## Description
Vérifie la performance, la fiabilité, les requêtes EF Core, le rendu Blazor, la résilience et la gestion des ressources dans une pull request.

## When to use
- Invoquer pour : Vérifie la performance, la fiabilité, les requêtes EF Core, le rendu Blazor, la résilience et la gestion des ressources dans une pull request.
- Utiliser uniquement lorsque la cible correspond au périmètre du skill.

## Inputs
- Cible, PR, issue ou chemins fournis par l’appelant.
- Contraintes humaines explicites et état Git vérifié.
- Périmètre limité à 20 fichiers au premier passage.

## Outputs
- Résultat vérifié correspondant à la mission du skill.
- Chemins exacts, preuves et décisions humaines restantes.
- Informations inconnues signalées par `À confirmer`.

## Steps
Vérifie les risques de performance et de fiabilité introduits par la PR.

## Accès aux données

- Requêtes N+1
- Chargement de colonnes ou d’entités inutiles
- Requêtes non paginées
- Absence de `AsNoTracking()` pour les lectures
- Requêtes dans une boucle
- Entités EF exposées directement à l’UI
- Absence de projection pour les lectures

## Rendu Blazor

- Rendu excessif ou recalculs coûteux
- Listes longues sans `Virtualize`
- Absence de `@key` sur les listes dynamiques
- Appels à `StateHasChanged()` inutiles
- État global mutable ou mal partagé
- Traitement lourd dans le markup Razor

## Résilience

- Appels HTTP sans timeout
- Absence de `CancellationToken`
- Absence de gestion d’erreur
- Retry infini ou mal configuré
- Ressources non libérées
- `HttpClient` créé manuellement au lieu d’`IHttpClientFactory`
- Absence de circuit breaker pour les intégrations critiques
- Erreurs silencieuses ou messages techniques exposés à l’utilisateur

## Format de commentaire

```md
### [BLOQUANT | IMPORTANT | SUGGESTION] Titre

**Fichier :** `chemin/du/fichier:Lxx-Lyy`

**Constat :** description précise.

**Impact :** performance, fiabilité, coût ou expérience utilisateur.

**Correction recommandée :** action concrète.

**Mesure recommandée :** métrique, benchmark ou test de charge.
```

## Règles

- Ne demande pas d’optimisation sans preuve ou risque clair.
- Distingue un problème mesuré d’un risque potentiel.
- Recommande une mesure avant toute optimisation coûteuse.
- Ne lance pas de test de charge.

## Exemples
- Exemple : appliquer le skill à une cible nommée explicitement par l’appelant.
- Exemple : produire une sortie vérifiée avec chemins exacts et commandes exécutées.

## Limites
- Inspecter au plus 20 fichiers sans exploration exhaustive.
- Arrêter après 3 tentatives ciblées infructueuses.
- Consulter `../../common/glossary.md` pour les termes communs.
