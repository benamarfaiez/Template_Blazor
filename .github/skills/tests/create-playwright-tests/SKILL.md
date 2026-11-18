---
name: create-playwright-tests
description: Crée ou met à jour des tests Playwright pour les parcours utilisateurs critiques d'une application Blazor.
---

# Créer des tests Playwright

Ajoute ou renforce les tests E2E nécessaires pour protéger les parcours
utilisateurs critiques.

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

- Place les tests dans le projet E2E existant.
- Utilise des données de test isolées et reproductibles.
- Vérifie le résultat visible par l’utilisateur.
- Attends les éléments ou états réels, sans délais fixes inutiles.
- Utilise les sélecteurs accessibles lorsque possible.
- Capture une preuve en cas d’échec lorsque l’outil le permet.
- Ne modifie pas le code de production.
- Ne crée pas de ressource externe sans validation explicite.

## Couverture minimale

Pour chaque parcours critique, couvre :

- Chemin nominal
- Validation ou erreur utilisateur
- État de chargement
- État vide, si applicable
- Échec serveur ou réseau, si pertinent