---
applyTo: "**/*.razor"
---

# Règles Blazor Razor

## Structure des composants

- Garde un composant focalisé sur une seule responsabilité.
- Extrais un sous-composant si le fichier dépasse 300 lignes ou contient
  plusieurs responsabilités.
- Nomme les composants avec un préfixe métier clair, par exemple
  `OrderSummaryCard.razor` plutôt que `Card1.razor`.
- Place les composants réutilisables dans `Shared/Components`.
- Place les pages dans `Pages` ou selon la structure existante du projet.

## Paramètres et événements

- Déclare les paramètres avec `[Parameter]` et traite-les comme immuables.
- Utilise `EventCallback<T>` pour les événements personnalisés.
- Ne modifie jamais un paramètre reçu du parent.
- Utilise des noms explicites : `OnOrderSaved`, `OrderChanged`, `IsLoading`.
- Évite les paramètres booléens multiples ; utilise une énumération ou un objet d’options lorsque la logique devient complexe.

## Cycle de vie

- Utilise `OnInitializedAsync` pour le chargement initial asynchrone.
- Utilise `OnParametersSetAsync` lorsque les paramètres influencent l’état.
- Utilise `OnAfterRenderAsync` uniquement pour le focus, la mesure DOM ou JS interop.
- Implémente `IDisposable` ou `IAsyncDisposable` pour libérer les abonnements, timers, connexions SignalR et références JS.
- Annule les opérations asynchrones avec `CancellationTokenSource` lorsque l’utilisateur peut naviguer ailleurs.

## Rendu et état

- Appelle `StateHasChanged()` uniquement après une mutation d’état hors du cycle de rendu standard.
- Évite les appels répétés à `StateHasChanged()`.
- Utilise `@key` pour les éléments de liste dynamiques.
- Utilise `Virtualize` pour les grandes listes.
- Évite les calculs coûteux dans le markup Razor.
- Précharge ou met en cache les données stables lorsque pertinent.

## Formulaires et validation

- Utilise `EditForm`, `DataAnnotationsValidator` et `ValidationSummary` lorsque le projet les utilise déjà.
- Valide toujours côté serveur, même si la validation client existe.
- Affiche des messages d’erreur clairs, spécifiques et associés aux champs.
- Désactive le bouton de soumission pendant le traitement, sans masquer l’état d’erreur.
- Conserve les données saisies en cas d’erreur de validation.

## Navigation et erreurs

- Utilise `NavigationManager` pour la navigation programmatique.
- Gère les URL invalides et les erreurs de navigation.
- Affiche un état de chargement pendant les opérations longues.
- Affiche un état vide explicite lorsqu’aucune donnée n’est disponible.
- Ne laisse jamais un utilisateur devant un écran blanc ou une erreur technique brute.

## Accessibilité

- Associe chaque champ à un label accessible.
- Fournis un `aria-label` ou un texte accessible pour les boutons icône.
- Assure un focus visible et une navigation clavier complète.
- Utilise des rôles ARIA uniquement lorsque le HTML sémantique ne suffit pas.
- Annonce les changements importants avec `aria-live` lorsque nécessaire.
- Vérifie le contraste, le zoom et le rendu mobile.