---
name: validate-refactoring-plan
description: Valide un plan de refactoring Blazor avant exécution : périmètre, dépendances, tests, risques, rollback, décisions humaines et absence de mélange de responsabilités.
allowed-tools:
  - read
  - search
  - glob
  - grep
---

# Valider un plan de refactoring

Vérifie que le plan est sûr, incrémental et exécutable avant de créer des
issues ou de lancer un refactoring.

## Contrôles obligatoires

### 1. Périmètre

- Chaque incrément a-t-il un objectif unique ?
- Le périmètre est-il limité et clairement délimité ?
- Les fichiers concernés sont-ils identifiés ?
- Y a-t-il des modifications hors périmètre ?

### 2. Comportement

- Le comportement utilisateur observable est-il préservé ?
- Les changements de comportement sont-ils explicitement demandés ?
- Les états de chargement, vide, erreur et succès sont-ils pris en compte ?

### 3. Tests

- Des tests de caractérisation existent-ils ou sont-ils prévus ?
- Les tests couvrent-ils les règles métier modifiées ?
- Les tests bUnit couvrent-ils le rendu et les interactions ?
- Les tests Playwright couvrent-ils les parcours critiques ?
- Les tests sont-ils déterministes, isolés et rapides ?

### 4. Dépendances

- Les incréments sont-ils réellement indépendants ?
- Les dépendances sont-elles explicites ?
- Un incrément peut-il être livré seul ?
- Y a-t-il un ordre d’exécution à respecter ?

### 5. Risques

- Les risques sont-ils identifiés ?
- Les mitigations sont-elles concrètes ?
- Les risques bloquants sont-isolés dans des incréments distincts ?
- Les décisions humaines sont-elles explicitement signalées ?

### 6. Rollback

- Chaque incrément possède-t-il un rollback ?
- Le rollback est-il simple et vérifiable ?
- Les migrations sont-elles isolées et réversibles ?
- Les contrats publics sont-ils préservés ?

### 7. Sécurité

- L’autorisation est-elle appliquée côté serveur ?
- Les entrées utilisateur sont-elles validées côté serveur ?
- Aucun secret n’est introduit dans le code ou les fichiers versionnés ?
- Aucune régression de sécurité n’est introduite ?

## Verdict

Termine par l’une de ces conclusions :

- `PLAN VALIDE`
- `PLAN VALIDE AVEC RÉSERVES`
- `PLAN INCOMPLET`
- `PLAN À RESTRUCTURER`
- `DÉCISION HUMAINE REQUISE`

## Règles

- Ne modifie aucun fichier.
- Ne crée pas d’issue.
- Ne crée pas de PR.
- Ne lance pas le refactoring.
- Signale tout incrément qui mélange refactoring, fonctionnalité, bugfix et migration.
- Signale tout incrément sans test ou sans rollback.