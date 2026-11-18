---
name: extract-blazor-component
description: Extrait un composant Blazor réutilisable à partir d'un composant volumineux ou dupliqué, sans changer le comportement utilisateur.
allowed-tools:
  - read
  - search
  - glob
  - grep
  - edit
  - write
  - terminal
---

# Extraire un composant Blazor

Extrais un composant réutilisable lorsqu’un composant est volumineux, dupliqué
ou possède plusieurs responsabilités.

## Quand extraire

Extrais un composant si :

- Le composant dépasse 300 lignes.
- Une portion d’UI est dupliquée.
- Le composant possède plusieurs responsabilités.
- Une carte, une ligne de tableau, un formulaire ou une section est réutilisable.
- Le composant mélange présentation, état complexe et logique métier.

## Méthode

1. Identifie la portion à extraire.
2. Détermine les paramètres nécessaires.
3. Détermine les événements à exposer.
4. Crée le composant dans l’emplacement approprié.
5. Remplace la portion dupliquée par le nouveau composant.
6. Ajoute ou confirme les tests bUnit.
7. Exécute le build et les tests.

## Règles

- Préserve strictement le comportement utilisateur observable.
- Utilise des paramètres immuables.
- Utilise `EventCallback<T>` pour les événements personnalisés.
- Utilise `@key` pour les listes dynamiques.
- Utilise `Virtualize` pour les listes longues.
- Évite les paramètres booléens multiples ; utilise un objet d’options ou une
  énumération si nécessaire.
- Ne modifie pas les contrats publics sans validation humaine.
- Ne lance pas de migration ou de commande destructive.

## Emplacement recommandé

```text
Shared/
  Components/
    Orders/
      OrderSummaryCard.razor
      OrderSummaryCard.razor.cs
```

## Exemple

Avant :

```razor
<div class="order-summary">
    <span>@order.Reference</span>
    <span>@order.Total</span>
</div>
```

Après :

```razor
<OrderSummaryCard Order="order" />
```

## Sortie attendue

- Composant créé ou modifié
- Paramètres et événements définis
- Tests bUnit ajoutés ou mis à jour
- Résultats de build et tests
- Risques résiduels