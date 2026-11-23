---
description: Crée ou met à jour des tests Playwright pour les parcours utilisateurs critiques d'une application Blazor.
name: create-playwright-tests
---

# Create Playwright Tests

## Description
Crée ou met à jour des tests Playwright pour les parcours utilisateurs critiques d'une application Blazor.

## When to use
- Invoquer pour : Crée ou met à jour des tests Playwright pour les parcours utilisateurs critiques d'une application Blazor.
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
- Si aucun projet E2E existant n’est disponible, ne crée pas de projet ni de
  configuration Playwright sans accord explicite. Arrête la création, indique
  dans les sorties les chemins attendus du projet et de la configuration ainsi
  que la décision humaine requise, et signale le blocage sous `À confirmer`.
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

## Exemples
- Exemple : appliquer le skill à une cible nommée explicitement par l’appelant.
- Exemple : produire une sortie vérifiée avec chemins exacts et commandes exécutées.

## Limites
- Inspecter au plus 20 fichiers sans exploration exhaustive.
- Arrêter après 3 tentatives ciblées infructueuses.
- Consulter `../glossary.md` pour les termes communs.
