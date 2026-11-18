---
description: Crée ou met à jour des tests xUnit de caractérisation pour les services, validateurs, règles métier, mappers et handlers Blazor.
name: create-xunit-tests
---

# Create Xunit Tests

## Description
Crée ou met à jour des tests xUnit de caractérisation pour les services, validateurs, règles métier, mappers et handlers Blazor.

## When to use
- Invoquer pour : Crée ou met à jour des tests xUnit de caractérisation pour les services, validateurs, règles métier, mappers et handlers Blazor.
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
Ajoute ou renforce les tests xUnit nécessaires pour verrouiller le comportement
actuel de la cible.

## Périmètre

Utilise xUnit pour :

- Services d’application
- Validateurs
- Règles métier
- Mappers
- Handlers
- Helpers
- Logique de calcul, transformation ou décision

## Règles

- Place les tests dans le projet de tests unitaires existant.
- Utilise Moq ou NSubstitute si le projet les utilise déjà.
- Mocke les dépendances externes, pas la logique métier.
- Utilise des données de test explicites et représentatives.
- Teste les cas nominaux, erreurs métier, entrées invalides et cas limites.
- Nomme les tests selon le scénario et le résultat attendu.
- Évite les dépendances au réseau, à l’horloge réelle, au système de fichiers
  et à une base de données partagée.
- Ne modifie pas le code de production.
- Ne supprime ni ne désactive un test existant.

## Structure d’un test

```csharp
[Fact]
public void <Méthode>_<Scénario>_<RésultatAttendu>()
{
    // Arrange
    // Act
    // Assert
}
```

## Couverture minimale

Couvre au minimum :

- Règle métier principale
- Validation et erreur métier
- Entrée invalide
- Cas limite
- Effet de bord important
- Exception attendue, si applicable

## Exemples
- Exemple : appliquer le skill à une cible nommée explicitement par l’appelant.
- Exemple : produire une sortie vérifiée avec chemins exacts et commandes exécutées.

## Limites
- Inspecter au plus 20 fichiers sans exploration exhaustive.
- Arrêter après 3 tentatives ciblées infructueuses.
- Consulter `../../common/glossary.md` pour les termes communs.
