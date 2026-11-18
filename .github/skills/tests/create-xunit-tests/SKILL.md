---
name: create-xunit-tests
description: Crée ou met à jour des tests xUnit de caractérisation pour les services, validateurs, règles métier, mappers et handlers Blazor.
---

# Créer des tests xUnit

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