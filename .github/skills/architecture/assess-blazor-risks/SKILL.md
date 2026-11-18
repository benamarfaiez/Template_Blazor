---
name: assess-blazor-risks
description: Évalue les risques techniques, de sécurité, de performance, de testabilité, d'accessibilité et de maintenabilité d'un projet Blazor.
allowed-tools:
  - read
  - search
---

# Évaluer les risques Blazor

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