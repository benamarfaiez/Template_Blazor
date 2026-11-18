---
name: qa-analyze-failures
description: Analyse les échecs de build, de tests ou de Playwright, distingue les bugs, régressions et problèmes de test, puis recommande une action sûre.
allowed-tools:
  - read
  - search
  - terminal
  - playwright-mcp
---

# Analyser les échecs QA

Analyse chaque échec sans masquer le problème.

## Classification

Classe chaque échec :

| Type | Signification | Action |
|---|---|---|
| `BUG EXISTANT` | Le test révèle un comportement incorrect du code | Signaler et demander une décision humaine |
| `RÉGRESSION` | Le test échoue après une modification de la PR | Corriger le code de production |
| `PROBLÈME DE TEST` | Le test, mock, fixture ou environnement est incorrect | Corriger le test ou la configuration |
| `ENVIRONNEMENT` | Dépendance, service, certificat ou configuration indisponible | Signaler et demander une clarification |
| `FLAKY` | Le test échoue de manière non déterministe | Isoler, stabiliser ou signaler le test |

## Méthode

1. Lis le message d’erreur et la stack trace.
2. Identifie le test, le composant ou le service concerné.
3. Relie l’échec au changement de la PR.
4. Vérifie si le test échouait avant la modification.
5. Détermine la cause racine.
6. Propose une correction concrète.
7. Ne relance pas aveuglément la même commande.

## Règles

- Ne supprime ou ne désactive jamais un test.
- Ne modifie pas une assertion pour masquer un échec.
- Ne contourne pas une erreur de sécurité, d’autorisation ou de validation.
- Ne corrige pas un bug de production sans validation explicite.
- Masque les secrets dans les extraits de log.