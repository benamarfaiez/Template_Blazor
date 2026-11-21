---
applyTo: "**/*.cs, **/*.razor"
---

# Règles de revue de code

Lors d’une revue, vérifie dans cet ordre :

1. Exactitude fonctionnelle et respect du besoin.
2. Sécurité, autorisation et protection des données.
3. Régression potentielle et couverture de tests.
4. Gestion des erreurs, annulation et libération des ressources.
5. Performance, requêtes EF Core et rendu Blazor.
6. Lisibilité, nommage, duplication et complexité.
7. Accessibilité et expérience utilisateur.
8. Documentation, migration et impact de déploiement.

## Format de commentaire

Pour chaque remarque :

- Indique la sévérité : `bloquant`, `important` ou `suggestion`.
- Explique le problème concret.
- Propose une correction actionnable.
- Cite le fichier et la ligne concernée.
- Ne propose pas de refactoring hors périmètre de la PR.

## Points bloquants

Traite comme bloquant :

- Une faille de sécurité ou une autorisation manquante.
- Une perte ou corruption potentielle de données.
- Une régression non couverte par un test.
- Une fuite de ressource, un deadlock ou une exception non gérée.
- Une requête N+1 majeure sur un parcours critique.
- Un secret exposé dans le code ou les logs.
- Une modification de schéma de base sans migration et rollback.