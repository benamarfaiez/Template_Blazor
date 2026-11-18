---
name: report-test-coverage
description: Produit un rapport de couverture, de risques résiduels et de décision avant refactoring pour une cible Blazor.
---

# Rapporter la couverture de tests

Produis un rapport Markdown clair après l’analyse et l’exécution des tests.

## Sections obligatoires

### 1. Cible analysée

- Composant, service ou module
- Fichiers principaux
- Comportement observable à préserver
- Dépendances et intégrations

### 2. État des tests

| Type | État | Fichiers | Lacunes |
|---|---|---|---|
| xUnit | Présent / absent / insuffisant | `tests/...` | … |
| bUnit | Présent / absent / insuffisant | `tests/...` | … |
| Playwright | Présent / absent / insuffisant | `tests/...` | … |

### 3. Tests ajoutés ou modifiés

Pour chaque test :

```md
### <Nom du test>

**Fichier :** `chemin/du/fichier`

**Objectif :** comportement vérifié.

**Scénario :** conditions initiales et action.

**Résultat attendu :** comportement observable attendu.

**Justification :** risque de régression couvert.
```

### 4. Résultats d’exécution

- Commandes exécutées
- Tests réussis, échoués et ignorés
- Échecs de test
- Bugs existants détectés
- Couverture estimée ou mesurée

### 5. Risques résiduels

Liste les zones insuffisamment couvertes et les tests prioritaires à ajouter.

### 6. Décision

Termine par l’une de ces conclusions :

- `PRÊT POUR REFACTORING`
- `TESTS COMPLÉMENTAIRES REQUIS`
- `BUG EXISTANT DÉTECTÉ`
- `CLARIFICATION REQUISE`

## Garde-fou

Ne déclare jamais `PRÊT POUR REFACTORING` si :

- Aucun test ne couvre la règle métier principale.
- Les tests existants échouent.
- Les tests dépendent de l’ordre, du réseau ou d’une base partagée.
- Les états de chargement, d’erreur ou de validation ne sont pas couverts.
- Un bug existant n’a pas été traité ou accepté explicitement.
- Une intégration externe, une autorisation ou un JavaScript interop critique n’est pas couvert.