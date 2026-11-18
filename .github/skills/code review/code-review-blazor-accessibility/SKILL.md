---
name: code-review-blazor-accessibility
description: Vérifie l'accessibilité, le clavier, les labels, les messages d'erreur, le focus et les états dynamiques dans une pull request Blazor.
allowed-tools:
  - read
  - search
  - github-mcp/get_pull_request_diff
  - playwright-mcp
---

# Vérifier l’accessibilité Blazor

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