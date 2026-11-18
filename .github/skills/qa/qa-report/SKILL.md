---
description: Produit un rapport de QA Blazor structuré avec résultats de tests, échecs, accessibilité, risques et décision finale.
allowed-tools:
  - read
  - search
  - github-mcp/get_pull_request
  - github-mcp/get_pull_request_diff
name: qa-report
---

# Qa Report

## Description
Produit un rapport de QA Blazor structuré avec résultats de tests, échecs, accessibilité, risques et décision finale.

## When to use
- Invoquer pour : Produit un rapport de QA Blazor structuré avec résultats de tests, échecs, accessibilité, risques et décision finale.
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
Produis un rapport Markdown clair et actionnable.

## Sections obligatoires

### 1. Résumé QA

- PR ou cible validée
- Variante Blazor
- Périmètre testé
- Décision finale
- Risque global : `Faible`, `Moyen` ou `Élevé`

### 2. Résultats des tests

| Type | Commande | Résultat | Tests réussis | Tests échoués | Commentaire |
|---|---|---|---|---|---|
| xUnit |  |  |  |  |  |
| bUnit |  |  |  |  |  |
| Playwright |  |  |  |  |  |

### 3. Échecs analysés

Pour chaque échec :

```md
### [BLOQUANT | IMPORTANT | SUGGESTION] Titre

**Type :** bug existant / régression / problème de test / environnement / flaky

**Localisation :** `chemin/du/fichier:Lxx-Lyy`

**Constat :** description précise.

**Impact :** conséquence utilisateur ou technique.

**Preuve :** extrait de log ou capture.

**Cause probable :** analyse de la cause racine.

**Action recommandée :** correction concrète.

**Test de validation :** test à relancer après correction.
```

### 4. Accessibilité

- Contrôles réussis
- Problèmes détectés
- Impact utilisateur
- Corrections recommandées

### 5. Risques résiduels

- Parcours non testés
- Couverture insuffisante
- Tests instables
- Dépendances externes non couvertes

### 6. Décision finale

Termine par l’une de ces conclusions :

- `VALIDÉ`
- `VALIDÉ AVEC RÉSERVES`
- `ÉCHEC DE QA`
- `BLOQUÉ`
- `CLARIFICATION REQUISE`

## Règles

- Ne déclare jamais `VALIDÉ` si un test bloquant échoue.
- Ne déclare jamais `VALIDÉ` si une régression n’est pas couverte.
- Ne déclare jamais `VALIDÉ` si un problème de sécurité critique est détecté.
- Ne déclare jamais `VALIDÉ` si une migration ou un déploiement est nécessaire sans validation humaine.
- Ne divulgue aucun secret dans le rapport.

## Exemples
- Exemple : appliquer le skill à une cible nommée explicitement par l’appelant.
- Exemple : produire une sortie vérifiée avec chemins exacts et commandes exécutées.

## Limites
- Inspecter au plus 20 fichiers sans exploration exhaustive.
- Arrêter après 3 tentatives ciblées infructueuses.
- Consulter `../../common/glossary.md` pour les termes communs.
