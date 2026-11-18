---
name: audit-blazor-security
description: Audit en lecture seule de la sécurité, de l'autorisation, de la validation, du JS interop, de la configuration et de l'exposition de secrets dans un projet Blazor.
allowed-tools:
  - read
  - search
---

# Auditer la sécurité Blazor

Analyse les risques de sécurité sans modifier le projet.

## Périmètre

- Autorisation des pages, composants, API et opérations
- Validation des entrées utilisateur
- Accès aux données et requêtes SQL
- Rendu HTML et XSS
- CSRF, cookies et en-têtes de sécurité
- Authentification, sessions et expiration
- JavaScript interop
- Téléversement de fichiers
- Journalisation et exposition de données sensibles
- Secrets, chaînes de connexion et clés API
- Dépendances vulnérables

## Contrôles à vérifier

- L’autorisation est appliquée côté serveur, pas seulement dans l’UI.
- Les politiques d’autorisation sont cohérentes.
- Toute entrée utilisateur est validée côté serveur.
- Les requêtes SQL utilisent des paramètres.
- Le contenu utilisateur est correctement encodé avant rendu.
- Les secrets ne sont pas dans le code, les tests ou les fichiers versionnés.
- Les logs ne contiennent pas de mot de passe, token, cookie ou donnée sensible.
- Les erreurs n’exposent pas de détails techniques internes.
- Les en-têtes de sécurité, HTTPS et cookies sont configurés correctement.
- Le JS interop valide les entrées et sorties.
- Les fichiers téléversés sont limités en taille, type et nombre.
- Les dépendances vulnérables sont identifiées.

## Format de constat

```md
### [BLOQUANT | ÉLEVÉ | MOYEN | FAIBLE] Titre

**Localisation :** `chemin/du/fichier:Lxx-Lyy`

**Constat :** description factuelle.

**Risque :** impact sécurité, données ou conformité.

**Preuve :** extrait court, sans secret.

**Action recommandée :** correction concrète.

**Priorité :** P0 / P1 / P2

**Tests nécessaires :** test de sécurité ou de régression.
```

## Règles

- Ne modifie aucun fichier.
- Ne divulgue aucun secret, token, mot de passe ou chaîne de connexion.
- Masque les valeurs sensibles dans les extraits de code.
- Ne tente pas d’exploiter une vulnérabilité.
- Signale immédiatement toute faille critique.