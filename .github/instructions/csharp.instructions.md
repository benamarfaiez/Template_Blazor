---
applyTo: "**/*.cs"
---

# Règles C#

## Conventions

- Suis `.editorconfig`, les analyzers Roslyn et les conventions existantes.
- Utilise le PascalCase pour les types, méthodes, propriétés et constantes publiques.
- Utilise le camelCase pour les variables locales et paramètres.
- Préfixe les interfaces par `I`.
- Utilise des noms explicites plutôt que des abréviations peu claires.
- Limite les méthodes à une responsabilité claire ; extrais une méthode si elle
  devient difficile à comprendre.

## Types et immuabilité

- Utilise `record` pour les DTO, messages et valeurs immuables.
- Utilise `readonly` lorsque possible.
- Préfére les collections immuables pour les données partagées.
- Utilise `required` pour les propriétés obligatoires lorsque le modèle le permet.
- Évite les classes publiques avec état mutable global.

## Asynchronisme

- Utilise `async`/`await` de bout en bout.
- Évite `async void`, sauf pour les gestionnaires d’événements UI lorsque nécessaire.
- Évite `.Result`, `.Wait()` et `.GetAwaiter().GetResult()`.
- Propage `CancellationToken` dans les opérations longues.
- Retourne `Task` plutôt que `async Task` lorsqu’aucun `await` n’est nécessaire.
- Ne bloque pas le thread UI ou le thread de rendu Blazor.

## Injection de dépendances

- Injecte les dépendances via le constructeur.
- Enregistre les services avec la durée de vie appropriée :
  `Singleton`, `Scoped` ou `Transient`.
- N’enregistre jamais un `DbContext` en singleton.
- Dépend d’interfaces pour les services testables.
- Évite le service locator et `IServiceProvider.GetService` dans la logique métier.

## Exceptions

- Utilise des exceptions spécifiques pour les erreurs métier.
- Ne capture pas `Exception` sans traitement explicite.
- Journalise l’exception avec son contexte avant de la relancer ou de la convertir.
- Ne retourne pas un booléen ambigu pour signaler une erreur complexe.
- Utilise `Result<T>` uniquement si le projet possède déjà ce pattern.

## Horloge, identifiants et aléatoire

- Injecte `TimeProvider` plutôt que d’appeler `DateTime.Now` directement.
- Injecte ou abstrais `Guid.NewGuid()`, `Random` et les sources d’aléatoire.
- Ne place pas de dépendance à l’horloge, au réseau ou au système de fichiers
  dans la logique métier pure.