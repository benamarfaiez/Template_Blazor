---
description: Vérifie la sécurité, l'autorisation, la validation, le JS interop, la gestion des secrets et l'exposition de données sensibles dans une pull request Blazor.
allowed-tools:
  - read
  - search
  - github-mcp/get_pull_request_diff
name: code-review-blazor-security
---

# Code Review Security

## Description
Vérifie la sécurité, l'autorisation, la validation, le JS interop, la gestion des secrets et l'exposition de données sensibles dans une pull request Blazor.

## When to use
- Invoquer pour : Vérifie la sécurité, l'autorisation, la validation, le JS interop, la gestion des secrets et l'exposition de données sensibles dans une pull request Blazor.
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

## Exemples
- Exemple : appliquer le skill à une cible nommée explicitement par l’appelant.
- Exemple : produire une sortie vérifiée avec chemins exacts et commandes exécutées.

## Limites
- Inspecter au plus 20 fichiers sans exploration exhaustive.
- Arrêter après 3 tentatives ciblées infructueuses.
- Consulter `../glossary.md` pour les termes communs.
