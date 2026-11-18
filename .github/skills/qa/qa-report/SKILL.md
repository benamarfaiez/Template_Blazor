---
name: qa-report
description: Produit un rapport de QA Blazor structuré avec résultats de tests, échecs, accessibilité, risques et décision finale.
allowed-tools:
  - read
  - search
  - github-mcp/get_pull_request
  - github-mcp/get_pull_request_diff
---

# Produire le rapport QA

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