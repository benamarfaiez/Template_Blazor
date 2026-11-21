---
description: Produit un rapport de couverture, de risques résiduels et de décision avant refactoring pour une cible Blazor.
name: report-test-coverage
---

# Report Test Coverage

## Description
Produit un rapport de couverture, de risques résiduels et de décision avant refactoring pour une cible Blazor.

## When to use
- Invoquer pour : Produit un rapport de couverture, de risques résiduels et de décision avant refactoring pour une cible Blazor.
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
- Les états de chargement, d’erreur ou de validation applicables au comportement sous test ne sont pas couverts; pour une cible service, les états UI inapplicables ne doivent pas bloquer la décision.
- Un bug existant n’a pas été traité ou accepté explicitement.
- Une intégration externe, une autorisation ou un JavaScript interop critique n’est pas couvert.

## Exemples
- Exemple : appliquer le skill à une cible nommée explicitement par l’appelant.
- Exemple : produire une sortie vérifiée avec chemins exacts et commandes exécutées.

## Limites
- Inspecter au plus 20 fichiers sans exploration exhaustive.
- Arrêter après 3 tentatives ciblées infructueuses.
- Consulter `../glossary.md` pour les termes communs.
