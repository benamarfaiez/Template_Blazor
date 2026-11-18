---
description: Extrait un composant Blazor réutilisable à partir d'un composant volumineux ou dupliqué, sans changer le comportement utilisateur.
allowed-tools:
  - read
  - search
  - glob
  - grep
  - edit
  - write
  - terminal
name: extract-blazor-component
---

# Extract Component

## Description
Extrait un composant Blazor réutilisable à partir d'un composant volumineux ou dupliqué, sans changer le comportement utilisateur.

## When to use
- Invoquer pour : Extrait un composant Blazor réutilisable à partir d'un composant volumineux ou dupliqué, sans changer le comportement utilisateur.
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

## Exemples
- Exemple : appliquer le skill à une cible nommée explicitement par l’appelant.
- Exemple : produire une sortie vérifiée avec chemins exacts et commandes exécutées.

## Limites
- Inspecter au plus 20 fichiers sans exploration exhaustive.
- Arrêter après 3 tentatives ciblées infructueuses.
- Consulter `../../common/glossary.md` pour les termes communs.
