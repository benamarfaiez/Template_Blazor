---
applyTo: "**/*.cs"
---

# Règles Entity Framework Core

- Dans les circuits Blazor côté serveur, utilise une `IDbContextFactory<TContext>` et crée un `DbContext` pour chaque opération afin d'éviter une durée de vie trop longue.
- Conserve une durée de vie `Scoped` pour les instances de `DbContext` lorsque le contexte s'inscrit dans la portée d'une requête HTTP.
- Utilise `AsNoTracking()` pour les requêtes en lecture seule.
- Utilise des projections `Select` pour ne charger que les colonnes nécessaires.
- Évite les requêtes N+1 ; utilise `Include`, `ThenInclude`, jointure ou requête explicite lorsque nécessaire.
- Pagine les requêtes pouvant retourner un grand volume de données.
- Utilise des requêtes paramétrées ; ne construis jamais une requête SQL par concaténation de chaînes.
- Utilise `ExecuteUpdateAsync` ou `ExecuteDeleteAsync` uniquement si le projet les utilise déjà et si le comportement attendu est clair.
- Isole les migrations dans une PR dédiée.
- Une migration doit être réversible ou accompagnée d’un plan de rollback.
- Ne modifie pas le schéma de base sans validation humaine.
- N’expose pas les entités EF directement dans les composants Blazor ou l’API.