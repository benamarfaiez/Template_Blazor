---
description: Évalue les risques techniques, de sécurité, de performance, de testabilité, d'accessibilité et de maintenabilité d'un projet Blazor.
allowed-tools:
  - read
  - search
name: assess-blazor-risks
---

# Assess Risks

## Description
Évalue les risques techniques, de sécurité, de performance, de testabilité, d'accessibilité et de maintenabilité d'un projet Blazor.

## When to use
- Invoquer pour : Évalue les risques techniques, de sécurité, de performance, de testabilité, d'accessibilité et de maintenabilité d'un projet Blazor.
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
Évalue les risques du projet à partir de la cartographie et du code.

## Domaines à évaluer

### Sécurité

- Autorisations manquantes ou incohérentes
- Validation serveur insuffisante
- Injection SQL, XSS, CSRF ou SSRF
- Secrets exposés
- JS interop non validé
- Données sensibles dans les logs ou erreurs
- Dépendances vulnérables

### Stabilité

- Gestion d’erreurs insuffisante
- Ressources non libérées
- Absence de `CancellationToken`
- Concurrence, race conditions ou état partagé mutable
- Échecs d’intégration non gérés
- Absence de timeout, retry ou circuit breaker

### Testabilité

- Absence de tests sur les règles métier critiques
- Tests dépendants du réseau, de l’horloge ou d’une base partagée
- Couplage fort rendant les tests difficiles
- Absence de tests bUnit ou Playwright sur les parcours critiques

### Performance

- Requêtes N+1
- Requêtes non paginées
- Chargement de données inutiles
- Rendu excessif
- Listes longues sans virtualisation
- Appels séquentiels parallélisables
- Absence de cache lorsque pertinent

### Maintenabilité

- Duplication
- Composants volumineux
- Logique métier dans l’UI
- Couplage fort entre couches
- Absence de séparation des responsabilités
- Documentation insuffisante

### Accessibilité

- Navigation clavier incomplète
- Labels ou messages d’erreur manquants
- Contraste insuffisant
- États de chargement ou d’erreur non annoncés

## Format de risque

```md
### [BLOQUANT | ÉLEVÉ | MOYEN | FAIBLE] Titre

**Domaine :** sécurité / stabilité / testabilité / performance / maintenabilité / accessibilité

**Localisation :** `chemin/du/fichier:Lxx-Lyy`

**Constat :** description factuelle.

**Impact :** conséquence concrète.

**Preuve :** extrait court du code.

**Hypothèse à confirmer :** élément nécessitant une validation.

**Action recommandée :** action incrémentale.

**Effort estimé :** S / M / L
```

## Règles

- Ne modifie aucun fichier.
- Ne présente pas une hypothèse comme un fait.
- Classe chaque risque par sévérité et domaine.
- Cite les preuves avec fichier et ligne.
- Ne propose pas encore de plan détaillé.

## Exemples
- Exemple : appliquer le skill à une cible nommée explicitement par l’appelant.
- Exemple : produire une sortie vérifiée avec chemins exacts et commandes exécutées.

## Limites
- Inspecter au plus 20 fichiers sans exploration exhaustive.
- Arrêter après 3 tentatives ciblées infructueuses.
- Consulter `../../common/glossary.md` pour les termes communs.
