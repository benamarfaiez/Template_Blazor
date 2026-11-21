---
description: Vérifie l'accessibilité, le clavier, les labels, les messages d'erreur, le focus et les états dynamiques dans une pull request Blazor.
allowed-tools:
  - read
  - search
  - github-mcp/get_pull_request_diff
  - playwright-mcp
name: code-review-blazor-accessibility
---

# Code Review Accessibility

## Description
Vérifie l'accessibilité, le clavier, les labels, les messages d'erreur, le focus et les états dynamiques dans une pull request Blazor.

## When to use
- Invoquer pour : Vérifie l'accessibilité, le clavier, les labels, les messages d'erreur, le focus et les états dynamiques dans une pull request Blazor.
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
Vérifie que la PR respecte les exigences d’accessibilité WCAG 2.2 AA.

## Contrôles obligatoires

- Chaque champ de formulaire possède un label accessible.
- Les boutons icône ont un nom accessible.
- La navigation clavier est complète.
- Le focus est visible.
- Les messages d’erreur sont clairs et associés aux champs concernés.
- Les états de chargement et d’erreur sont annoncés lorsque nécessaire.
- Les contrastes sont suffisants.
- Les composants interactifs sont utilisables au clavier.
- Les changements dynamiques importants sont annoncés avec `aria-live` lorsque pertinent.
- Les icônes décoratives sont masquées aux technologies d’assistance.
- Les textes ne reposent pas uniquement sur la couleur pour transmettre une information.

## Format de commentaire

```md
### [BLOQUANT | IMPORTANT | SUGGESTION] Titre

**Fichier :** `chemin/du/fichier:Lxx-Lyy`

**Constat :** problème d’accessibilité.

**Impact utilisateur :** personne concernée et conséquence.

**Correction recommandée :** correction HTML, ARIA ou CSS concrète.

**Test recommandé :** test clavier, lecteur d’écran ou Playwright.
```

## Règles

- Ne propose pas d’ARIA inutile lorsque le HTML sémantique suffit.
- Vérifie le rendu réel avec Playwright lorsque la PR modifie une interaction ou
  un état visuel important.
- Ne lance pas de test destructif.

## Exemples
- Exemple : appliquer le skill à une cible nommée explicitement par l’appelant.
- Exemple : produire une sortie vérifiée avec chemins exacts et commandes exécutées.

## Limites
- Inspecter au plus 20 fichiers sans exploration exhaustive.
- Arrêter après 3 tentatives ciblées infructueuses.
- Consulter `../glossary.md` pour les termes communs.
