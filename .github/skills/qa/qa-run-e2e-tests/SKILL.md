---
name: qa-run-e2e-tests
description: Exécute les tests Playwright des parcours utilisateurs critiques d'une application Blazor et capture les preuves en cas d'échec.
allowed-tools:
  - read
  - search
  - glob
  - terminal
  - playwright-mcp
---

# Exécuter les tests E2E

Exécute les tests Playwright pertinents pour les parcours utilisateurs critiques.

## Commandes

Utilise la commande déjà configurée dans le dépôt, par exemple :

```bash
dotnet test tests/<App>.Tests.E2E
dotnet test tests/<App>.Tests.E2E --filter "FullyQualifiedName~<NomDuParcours>"
```

## Périmètre

Teste uniquement les parcours critiques :

- Connexion, déconnexion et expiration de session
- Création, consultation, modification et suppression
- Recherche, filtrage, pagination et tri
- Soumission de formulaire et validation
- Gestion des erreurs et états de chargement
- Navigation entre pages principales
- Opérations sensibles : paiement, suppression, export, changement de mot de passe

## Règles

- Utilise des données de test isolées et reproductibles.
- Vérifie le résultat visible par l’utilisateur.
- Attends les éléments ou états réels, sans délais fixes inutiles.
- Utilise des sélecteurs accessibles lorsque possible.
- Capture une preuve en cas d’échec lorsque l’outil le permet.
- Ne lance pas de test de charge.
- Ne crée pas de ressource externe sans validation explicite.
- Ne teste pas les parcours non critiques si le temps d’exécution devient prohibitif.

## Couverture minimale

Pour chaque parcours critique, vérifie :

- Chemin nominal
- Validation ou erreur utilisateur
- État de chargement
- État vide, si applicable
- Échec serveur ou réseau, si pertinent