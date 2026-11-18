---
name: code-review-blazor-security
description: Vérifie la sécurité, l'autorisation, la validation, le JS interop, la gestion des secrets et l'exposition de données sensibles dans une pull request Blazor.
allowed-tools:
  - read
  - search
  - github-mcp/get_pull_request_diff
---

# Vérifier la sécurité Blazor

Vérifie les risques de sécurité introduits ou non corrigés par la PR.

## Contrôles obligatoires

- L’autorisation est appliquée côté serveur.
- Les politiques d’autorisation sont cohérentes.
- Toute entrée utilisateur est validée côté serveur.
- Les requêtes SQL utilisent des paramètres.
- Le contenu utilisateur est correctement encodé avant rendu.
- Les secrets ne sont pas exposés dans le code, les tests ou les fichiers versionnés.
- Les logs ne contiennent pas de mot de passe, token, cookie ou donnée sensible.
- Les erreurs n’exposent pas de détails techniques internes.
- Le JS interop valide les entrées et sorties.
- Les fichiers téléversés sont limités en taille, type et nombre.
- Les dépendances ajoutées sont nécessaires et ne présentent pas de risque connu.
- Les changements de configuration ne fragilisent pas HTTPS, CORS, cookies ou en-têtes de sécurité.

## Points bloquants

Traite comme bloquant :

- Absence d’autorisation sur une opération sensible.
- Injection SQL ou XSS potentielle.
- Secret exposé dans le code, les logs ou un fichier versionné.
- Donnée sensible écrite dans un log.
- Validation serveur absente sur une donnée critique.
- Désactivation d’un contrôle de sécurité pour faire passer un test.
- Exposition d’une API ou d’un endpoint sensible sans autorisation.

## Format de commentaire

```md
### [BLOQUANT | IMPORTANT | SUGGESTION] Titre

**Fichier :** `chemin/du/fichier:Lxx-Lyy`

**Constat :** description précise.

**Risque :** impact sécurité, données ou conformité.

**Correction recommandée :** action concrète.

**Test recommandé :** test de sécurité ou de régression.
```

## Règles

- Ne tente pas d’exploiter une vulnérabilité.
- Masque les secrets dans les extraits de code.
- Ne divulgue aucun token, mot de passe ou chaîne de connexion.
- Signale immédiatement toute faille critique.