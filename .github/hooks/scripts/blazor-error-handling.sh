#!/usr/bin/env bash
set -euo pipefail

input=$(cat)

error_message=$(printf '%s' "$input" | jq -r '.error // empty')
error_context=$(printf '%s' "$input" | jq -r '.errorContext // empty')
recoverable=$(printf '%s' "$input" | jq -r '.recoverable // empty')
tool_name=$(printf '%s' "$input" | jq -r '.toolName // empty')
tool_args=$(printf '%s' "$input" | jq -r '.toolArgs // empty')

# 1. Journalisation locale sans donnée sensible
log_dir=".copilot-agent-logs"
mkdir -p "$log_dir"
log_file="$log_dir/errors-$(date +%Y-%m-%d).log"

printf '%s\n' "$input" \
  | jq -c 'del(.toolArgs.environment, .toolArgs.env, .toolArgs.secrets)' \
  >> "$log_file" 2>/dev/null || true

# 2. Conseils selon le type d’erreur
context=()

case "$error_context" in
  "tool_execution")
    context+=(
      "Une erreur est survenue pendant l'exécution d'un outil."
      "Ne relance pas aveuglément la même commande."
      "Analyse d'abord le message d'erreur, la commande exécutée et les fichiers concernés."
    )
    ;;
  "model_call")
    context+=(
      "Une erreur est survenue lors d'un appel au modèle."
      "Ne répète pas automatiquement la requête."
      "Simplifie la tâche, vérifie le contexte disponible ou demande une clarification."
    )
    ;;
  "system")
    context+=(
      "Une erreur système est survenue."
      "Vérifie l'environnement, les permissions, l'espace disque et la disponibilité des outils requis."
      "Ne tente pas de contourner une erreur système."
    )
    ;;
  "user_input")
    context+=(
      "L'entrée utilisateur est invalide ou ambiguë."
      "Demande une clarification plutôt que de faire une hypothèse."
    )
    ;;
  *)
    context+=(
      "Une erreur inconnue est survenue."
      "Analyse le contexte et demande une clarification si la cause n'est pas évidente."
    )
    ;;
esac

# 3. Erreurs .NET fréquentes
if printf '%s' "$error_message" | grep -qiE 'NU[0-9]{4}|restore failed|package.*not found|nuget'; then
  context+=(
    "Erreur liée à NuGet ou à la restauration des packages."
    "- Vérifie la version .NET, les sources NuGet et la connectivité réseau."
    "- Ne modifie pas une dépendance majeure sans validation humaine."
    "- Si une dépendance est vulnérable, signale-la et propose une mise à jour contrôlée."
  )
fi

if printf '%s' "$error_message" | grep -qiE 'CS[0-9]{4}|compilation error|build failed|MSB[0-9]{4}'; then
  context+=(
    "Erreur de compilation .NET détectée."
    "- Analyse les premières erreurs du compilateur."
    "- Corrige la cause racine, pas seulement le symptôme."
    "- Ne désactive pas une règle d'analyse, un avertissement ou un test pour masquer l'échec."
    "- Après correction, exécute dotnet build --no-restore et les tests pertinents."
  )
fi

if printf '%s' "$error_message" | grep -qiE 'test failed|failed!|xunit|bunit|playwright|assert'; then
  context+=(
    "Échec de test détecté."
    "- Classe l'échec : bug existant, régression ou problème de test."
    "- Ne supprime ou ne désactive pas un test pour faire passer la suite."
    "- Pour une régression, corrige le code de production."
    "- Pour un problème de test, corrige le test, la fixture ou le mock."
    "- Pour un bug existant, signale-le et attends une décision humaine."
  )
fi

if printf '%s' "$error_message" | grep -qiE 'ef core|migration|database|sql|connection refused|timeout'; then
  context+=(
    "Erreur potentielle liée à la base de données ou à EF Core."
    "- Ne lance jamais une migration, une suppression de base ou une suppression de migration."
    "- Vérifie la chaîne de connexion, les migrations en attente et les permissions."
    "- Ne journalise pas de chaîne de connexion ou de secret."
    "- Demande une validation humaine avant toute action destructive."
  )
fi

if printf '%s' "$error_message" | grep -qiE 'http|timeout|network|socket|certificate|tls|ssl'; then
  context+=(
    "Erreur réseau ou HTTP détectée."
    "- Vérifie l'URL, le timeout, le certificat et la connectivité."
    "- Ne désactive jamais la validation TLS."
    "- Ne relance pas une requête en boucle sans politique de retry contrôlée."
    "- Vérifie que CancellationToken est correctement utilisé."
  )
fi

if printf '%s' "$error_message" | grep -qiE 'permission denied|access denied|unauthorized|forbidden|401|403'; then
  context+=(
    "Erreur d'autorisation ou de permission détectée."
    "- Ne contourne pas l'autorisation pour faire passer une action."
    "- Vérifie les politiques d'autorisation, les rôles et les permissions côté serveur."
    "- Signale le problème plutôt que de modifier les règles de sécurité."
  )
fi

if printf '%s' "$error_message" | grep -qiE 'secret|token|password|api key|connection string'; then
  context+=(
    "ALERTE : le message d'erreur peut contenir une donnée sensible."
    "- Ne copie pas le secret dans un fichier, un log, une PR ou un message utilisateur."
    "- Masque immédiatement la valeur sensible."
    "- Vérifie que la configuration utilise un gestionnaire de secrets ou des variables d'environnement."
  )
fi

# 4. Conseil de reprise
if [ "$recoverable" = "true" ]; then
  context+=(
    "L'erreur peut potentiellement être récupérée."
    "- Corrige d'abord la cause racine."
    "- Relance uniquement l'action nécessaire, avec un périmètre réduit."
    "- Vérifie le résultat avant de poursuivre."
  )
else
  context+=(
    "L'erreur n'est pas considérée comme récupérable automatiquement."
    "- Ne relance pas l'action."
    "- Produis un diagnostic clair et demande une décision humaine."
  )
fi

# 5. Contexte spécifique à l'outil
if [ -n "$tool_name" ]; then
  context+=(
    "Outil concerné : $tool_name."
    "Arguments concernés : $tool_args."
    "Vérifie que les arguments ne contiennent pas de secret avant de les partager."
  )
fi

# 6. Format JSON
json_context=$(printf '%s\n' "${context[@]}" | jq -Rs .)
printf '{"additionalContext":%s}\n' "$json_context"