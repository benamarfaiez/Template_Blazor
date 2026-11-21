---
name: audit-blazor-quality
description: Audite la qualité du code Blazor (composants, services, DTO, erreurs, tests, accessibilité, documentation) d'une cible fournie. Lecture seule. Invoquer pour "audite la qualité", "vérifie la maintenabilité", "contrôle les conventions", ou sur une PR qui modifie des composants/services. Ne couvre pas sécurité (audit-blazor-security), performance (audit-blazor-performance).
allowed-tools: [read, search]
---

# Audit Quality

## Préconditions
Cible fournie (PR, issue, chemins). Sinon → arrêter et signaler `cible absente`.

## Inputs / Outputs
Voir `../../common/report-format.md` (format de finding, sévérités, preuves).
Spécifique : catégorie ∈ {composants, services, DTO, erreurs, tests, accessibilité, doc}.

## Steps
1. Lire la cible (max 20 fichiers, 1 passe).
2. Pour chaque catégorie ci-dessous, identifier les findings avérés.
3. Preuve : extrait 1–5 lignes avec `chemin/fichier:Lxx-Lyy`.
4. S'arrêter après 3 tentatives ciblées infructueuses.

## Critères (spécifiques à ce skill)

| Catégorie | Signaux à rechercher |
|---|---|
| **Composants** | > 150 lignes, logique métier dans l'UI, duplication, `[Parameter]` non typés |
| **Services** | Responsabilités multiples, couplage fort UI, interface absente, effets de bord non documentés |
| **DTO/Entités** | Anémiques ou surchargés, validation absente/incohérente, mutabilité non justifiée |
| **Erreurs** | try/catch vides ou trop larges, exceptions silencieuses, messages non actionnables |
| **Tests** | Règles métier critiques non testées, tests fragiles (temps/réseau), absence bUnit/Playwright sur parcours critiques |
| **Accessibilité** | ARIA manquants, navigation clavier incomplète, contraste insuffisant |
| **Documentation** | README obsolète, commentaires trompeurs, API publiques non documentées |

## Règles
- Lecture seule : ne modifie aucun fichier.
- Hypothèse ≠ fait : tout élément non vérifié → `À confirmer`.
- Chaque finding classé (sévérité + catégorie) et cité avec fichier:ligne.
- Les 7 catégories apparaissent dans la sortie, même sans finding
  (ligne : `Aucun finding — <catégorie>`).
- Termes communs : `../glossary.md`.