---
name: extract-blazor-business-logic
description: Extrait la validation, les règles métier et les appels externes d'un composant Blazor Razor vers des services testables, sans changer le comportement utilisateur.
allowed-tools:
  - read
  - search
  - glob
  - grep
  - edit
  - write
  - terminal
---

# Extraire la logique métier Blazor

Extrais la logique métier d’un composant Razor vers des services testables.

## Périmètre

Extrais uniquement :

- Validation métier
- Règles métier
- Calculs
- Transformations de données
- Appels à des services
- Orchestration de cas d’usage
- Gestion complexe des erreurs métier

Ne extrais pas :

- Présentation
- Navigation
- État visuel simple
- Gestion d’événements UI purement locaux
- Texte ou libellés d’interface

## Méthode

1. Identifie la logique métier dans le composant Razor.
2. Identifie les dépendances nécessaires.
3. Crée ou réutilise une interface de service.
4. Crée ou réutilise un service d’application.
5. Déplace la logique sans changer son comportement.
6. Injecte le service dans le composant via `@inject` ou le constructeur.
7. Ajoute ou confirme les tests xUnit du service.
8. Ajoute ou confirme les tests bUnit du composant.
9. Exécute le build et les tests.

## Règles

- Préserve strictement le comportement utilisateur observable.
- Utilise l’injection par constructeur ou `@inject`.
- Dépend d’interfaces pour faciliter les tests.
- Évite les dépendances circulaires.
- N’expose pas les entités EF directement à l’UI.
- Utilise des DTO à la frontière des couches.
- Propage `CancellationToken` dans les opérations asynchrones longues.
- Ne modifie pas les contrats publics sans validation humaine.
- Ne lance pas de migration ou de commande destructive.

## Structure cible

```text
Services/
  Orders/
    IOrderValidator.cs
    OrderValidator.cs
    IOrderService.cs
    OrderService.cs
```

## Exemple

Avant :

```razor
@if (!IsValid(order))
{
    <span>Commande invalide</span>
}
```

Après :

```razor
@if (!await orderValidator.IsValidAsync(order, cancellationToken))
{
    <span>Commande invalide</span>
}
```

## Sortie attendue

- Fichiers modifiés et créés
- Logique extraite
- Interface et service créés ou réutilisés
- Tests ajoutés ou mis à jour
- Résultats de build et tests
- Risques résiduels