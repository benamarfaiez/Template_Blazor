---
name: assess-blazor-risks
description: Audit de risques (sécurité, stabilité, testabilité, performance, 
  maintenabilité, accessibilité) d'une cible Blazor fournie. Lecture seule.
allowed-tools: [read, search]
---

# Assess Risks

## Préconditions
Cible fournie (PR, issue, ou liste de chemins). Sinon → arrêter et 
signaler `cible absente`.

## Inputs
- Cible : PR, issue, ou chemins (max 20 fichiers au premier passage).
- Contraintes humaines explicites et état Git vérifié.

## Outputs
Rapport listant les 6 domaines, chacun avec ses risques au format ci-dessous 
ou la ligne `Aucun risque détecté`. Informations non vérifiables → `À confirmer`.

## Steps
1. Vérifier la précondition (cible fournie).
2. Évaluer les domaines dans cet ordre : Sécurité, Stabilité, Testabilité, 
   Performance, Maintenabilité, Accessibilité.
3. Pour chaque risque : preuve 1–5 lignes avec `chemin/fichier:Lxx-Lyy`.
4. S'arrêter après 3 tentatives ciblées infructueuses ou 20 fichiers lus.

## Domaines (ordre = priorité)

| # | Domaine | Signaux |
|---|---|---|
| 1 | **Sécurité** | Autorisations manquantes, validation serveur absente, injection (SQL/XSS/CSRF/SSRF), secrets exposés, JS interop non validé, données sensibles en logs, dépendances vulnérables |
| 2 | **Stabilité** | Gestion d'erreurs absente, ressources non libérées, `CancellationToken` manquant, race conditions, échecs d'intégration non gérés, timeout/retry/circuit breaker absents |
| 3 | **Testabilité** | Règles métier critiques non testées, tests dépendants du réseau/horloge/BDD partagée, couplage fort, absence bUnit/Playwright sur parcours critiques |
| 4 | **Performance** | N+1, requêtes non paginées, données inutiles, rendu excessif, listes sans virtualisation, appels séquentiels parallélisables, cache absent |
| 5 | **Maintenabilité** | Duplication, composants volumineux, logique métier dans l'UI, couplage inter-couches, séparation des responsabilités absente, doc insuffisante |
| 6 | **Accessibilité** | Navigation clavier incomplète, labels/erreurs manquants, contraste insuffisant, états de chargement/erreur non annoncés |

## Format d'un risque

```md
### [BLOQUANT | ÉLEVÉ | MOYEN | FAIBLE] Titre
**Domaine :** sécurité | stabilité | testabilité | performance | maintenabilité | accessibilité
**Localisation :** `chemin/fichier:Lxx-Lyy`
**Constat :** fait observable.
**Impact :** conséquence concrète.
**Preuve :** extrait 1–5 lignes numérotées.
**Hypothèse à confirmer :** élément à valider, ou "aucune".
**Action recommandée :** action incrémentale.
**Effort :** S | M | L