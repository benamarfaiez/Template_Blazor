---
applyTo: "**/*.cs, **/*.razor, **/*.json, **/*.yml"
---

# Règles de sécurité

- Ne stocke jamais de secret, clé API, mot de passe, token ou chaîne de connexion dans le code, les tests ou les fichiers versionnés.
- Utilise des variables d’environnement, Azure Key Vault ou un gestionnaire de secrets.
- Valide et autorise chaque requête côté serveur.
- Utilise l’autorisation basée sur des politiques ASP.NET Core.
- Ne fais jamais confiance à l’identité, au rôle ou au prix envoyé par le client.
- Valide toutes les entrées utilisateur et toutes les données externes.
- Utilise des requêtes paramétrées pour prévenir l’injection SQL.
- Encode le contenu utilisateur avant rendu HTML.
- Protège les formulaires contre le CSRF lorsque applicable.
- Applique HTTPS, HSTS et les en-têtes de sécurité recommandés.
- Limite la taille, le type et le nombre de fichiers téléversés.
- Journalise les événements de sécurité sans exposer de données sensibles.
- Signale immédiatement toute dépendance vulnérable ou méthode de chiffrement obsolète.