---
description: Vérifie la couverture, la qualité, la robustesse et la pertinence des tests ajoutés ou modifiés dans une pull request Blazor.
allowed-tools:
  - read
  - search
  - github-mcp/get_pull_request_diff
name: code-review-blazor-tests
---

# Code Review Tests

## Description
Vérifie la couverture, la qualité, la robustesse et la pertinence des tests ajoutés ou modifiés dans une pull request Blazor.

## When to use
- Invoquer pour : Vérifie la couverture, la qualité, la robustesse et la pertinence des tests ajoutés ou modifiés dans une pull request Blazor.
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
Vérifie que la PR est correctement couverte par des tests pertinents.

## Contrôles obligatoires

- Les tests couvrent le comportement modifié.
- Les tests couvrent les cas nominaux, erreurs et cas limites.
- Les tests ne dépendent pas de l’ordre, du réseau, de l’horloge ou d’une base partagée.
- Les tests valident le comportement observable, pas les détails d’implémentation.
- Les tests sont déterministes, isolés et rapides.
- Les tests existants ne sont pas supprimés sans justification explicite.
- Les tests ne sont pas désactivés pour faire passer la CI.
- Les assertions ne sont pas affaiblies pour masquer une régression.
- Les mocks sont limités aux dépendances externes.
- Les fixtures ne contiennent pas de données personnelles réelles ou de secrets.

## Couverture attendue

Pour une modification de composant Razor :

- Rendu des données principales
- Interaction principale
- État de chargement
- État vide
- État d’erreur
- Validation ou message d’erreur

Pour une modification de service :

- Règle métier principale
- Validation
- Erreur métier
- Entrée invalide
- Cas limite
- Effet de bord important

Pour une modification d’API ou de parcours critique :

- Test Playwright du parcours utilisateur
- Gestion des erreurs
- État de chargement
- Autorisation, si applicable

## Format de commentaire

```md
### [BLOQUANT | IMPORTANT | SUGGESTION] Titre

**Fichier :** `chemin/du/fichier:Lxx-Lyy`

**Constat :** couverture ou qualité de test insuffisante.

**Impact :** risque de régression non détectée.

**Correction recommandée :** test à ajouter ou mettre à jour.

**Scénario à couvrir :** comportement attendu et cas limite.
```

## Exemples
- Exemple : appliquer le skill à une cible nommée explicitement par l’appelant.
- Exemple : produire une sortie vérifiée avec chemins exacts et commandes exécutées.

## Limites
- Inspecter au plus 20 fichiers sans exploration exhaustive.
- Arrêter après 3 tentatives ciblées infructueuses.
- Consulter `../glossary.md` pour les termes communs.
