---
description: Valide l'accessibilité des écrans Blazor modifiés : navigation clavier, labels, focus, messages d'erreur, contrastes et états dynamiques.
allowed-tools:
  - read
  - search
  - playwright-mcp
name: qa-validate-accessibility
---

# Qa Validate Accessibility

## Description
Valide l'accessibilité des écrans Blazor modifiés : navigation clavier, labels, focus, messages d'erreur, contrastes et états dynamiques.

## When to use
- Invoquer pour : Valide l'accessibilité des écrans Blazor modifiés : navigation clavier, labels, focus, messages d'erreur, contrastes et états dynamiques.
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
Vérifie que les écrans modifiés respectent WCAG 2.2 niveau AA.

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
- Les textes ne reposent pas uniquement sur la couleur.

## Méthode

1. Identifie les pages ou composants modifiés.
2. Ouvre les parcours concernés avec Playwright.
3. Navigue au clavier.
4. Vérifie les labels, rôles, focus et messages d’erreur.
5. Capture une preuve en cas d’échec lorsque l’outil le permet.
6. Propose une correction HTML, ARIA ou CSS concrète.

## Règles

- Ne propose pas d’ARIA inutile lorsque le HTML sémantique suffit.
- Ne lance pas de test destructif.
- Ne modifie pas le code de production.
- Signale les problèmes d’accessibilité bloquants séparément des suggestions.

## Exemples
- Exemple : appliquer le skill à une cible nommée explicitement par l’appelant.
- Exemple : produire une sortie vérifiée avec chemins exacts et commandes exécutées.

## Limites
- Inspecter au plus 20 fichiers sans exploration exhaustive.
- Arrêter après 3 tentatives ciblées infructueuses.
- Consulter `../glossary.md` pour les termes communs.
