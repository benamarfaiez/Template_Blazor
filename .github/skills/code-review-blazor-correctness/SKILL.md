---
description: Vérifie l'exactitude fonctionnelle, la gestion d'état, le cycle de vie Blazor, les erreurs, l'annulation et les régressions dans une pull request.
tools:
  - read
  - search
  - github-mcp/get_pull_request_diff
name: code-review-blazor-correctness
---

# Code Review Correctness

## Description
Vérifie l'exactitude fonctionnelle, la gestion d'état, le cycle de vie Blazor, les erreurs, l'annulation et les régressions dans une pull request.

## When to use
- Invoquer pour : Vérifie l'exactitude fonctionnelle, la gestion d'état, le cycle de vie Blazor, les erreurs, l'annulation et les régressions dans une pull request.
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
Vérifie que la PR implémente correctement le comportement attendu sans
introduire de régression.

## Points à vérifier

### Comportement

- Le changement répond-il à l’objectif de la PR ?
- Le comportement utilisateur existant est-il préservé ?
- Les cas nominaux, erreurs et cas limites sont-ils gérés ?
- Les messages d’erreur sont-ils clairs et appropriés ?
- Les états de chargement, vide et succès sont-ils gérés ?

### Blazor

- Les paramètres de composant sont-ils immuables ?
- Les `EventCallback<T>` sont-ils correctement utilisés ?
- `StateHasChanged()` est-il utilisé uniquement lorsque nécessaire ?
- Le cycle de vie est-il correctement géré ?
- Les ressources sont-elles libérées avec `IDisposable` ou `IAsyncDisposable` ?
- Les abonnements, timers, connexions SignalR et références JS sont-ils libérés ?
- `CancellationToken` est-il propagé lorsque pertinent ?
- Les appels asynchrones évitent-ils `.Result`, `.Wait()` et `.GetAwaiter().GetResult()` ?

### État et concurrence

- L’état est-il correctement isolé ou partagé ?
- Y a-t-il un risque de race condition ?
- Une requête ou action peut-elle être exécutée deux fois ?
- L’état est-il correct après navigation, refresh ou expiration de session ?

### Gestion des erreurs

- Les erreurs sont-elles journalisées avec un contexte utile ?
- Les erreurs techniques sont-elles masquées à l’utilisateur ?
- Les erreurs métier sont-elles distinguées des erreurs techniques ?
- Les exceptions sont-elles traitées ou relancées de manière appropriée ?

## Format de commentaire

```md
### [BLOQUANT | IMPORTANT | SUGGESTION] Titre

**Fichier :** `chemin/du/fichier:Lxx-Lyy`

**Constat :** description précise.

**Impact :** régression, bug, maintenabilité ou robustesse.

**Correction recommandée :** action concrète.

**Test recommandé :** test à ajouter ou mettre à jour.
```

## Exemples
- Exemple : appliquer le skill à une cible nommée explicitement par l’appelant.
- Exemple : produire une sortie vérifiée avec chemins exacts et commandes exécutées.

## Limites
- Inspecter au plus 20 fichiers sans exploration exhaustive.
- Arrêter après 3 tentatives ciblées infructueuses.
- Consulter `../glossary.md` pour les termes communs.
