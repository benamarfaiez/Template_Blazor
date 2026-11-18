#!/usr/bin/env bash
set -euo pipefail

input=$(cat)
reason=$(printf '%s' "$input" | jq -r '.reason // empty')
final_message=$(printf '%s' "$input" | jq -r '.finalMessage // empty')
cwd=$(printf '%s' "$input" | jq -r '.cwd // "."')

cd "$cwd"

# 1. Journalisation locale, sans donnée sensible
log_dir=".copilot-agent-logs"
mkdir -p "$log_dir"
log_file="$log_dir/sessions-$(date +%Y-%m-%d).log"

printf '%s\n' "$input" \
  | jq -c 'del(.toolArgs.environment, .toolArgs.env, .toolArgs.secrets)' \
  >> "$log_file" 2>/dev/null || true

# 2. Nettoyage prudent des artefacts temporaires connus
rm -rf .copilot-test-artifacts 2>/dev/null || true
rm -rf .copilot-playwright-artifacts 2>/dev/null || true
rm -rf TestResults/temp 2>/dev/null || true

# 3. Détecter les fichiers modifiés
changed_files=$(git diff --name-only --diff-filter=ACMR 2>/dev/null || true)
staged_files=$(git diff --cached --name-only --diff-filter=ACMR 2>/dev/null || true)

context=()

# 4. Résumé de fin de session
context+=(
  "Session terminée."
  "Raison de fin : ${reason:-inconnue}."
)

if [ -n "$changed_files" ]; then
  context+=(
    "Fichiers modifiés non commités :"
    "$changed_files"
  )
fi

if [ -n "$staged_files" ]; then
  context+=(
    "Fichiers indexés :"
    "$staged_files"
  )
fi

# 5. Vérification des fichiers sensibles
sensitive_files=$(printf '%s\n%s' "$changed_files" "$staged_files" \
  | sort -u \
  | grep -E '\.env$|\.pfx$|\.key$|secret|credential|password|connectionstring' \
  || true)

if [ -n "$sensitive_files" ]; then
  context+=(
    "ALERTE : des fichiers potentiellement sensibles ont été modifiés :"
    "$sensitive_files"
    "Ne les committe pas et vérifie qu'ils ne contiennent aucun secret."
  )
fi

# 6. Vérification des migrations
migration_files=$(printf '%s\n%s' "$changed_files" "$staged_files" \
  | sort -u \
  | grep -E 'Migrations/|\.edmx' \
  || true)

if [ -n "$migration_files" ]; then
  context+=(
    "ALERTE : des migrations ou fichiers de base de données ont été modifiés :"
    "$migration_files"
    "Ne les applique pas automatiquement."
    "Demande une validation humaine avant dotnet ef database update."
    "Vérifie la réversibilité, les données existantes et le plan de rollback."
  )
fi

# 7. Vérification des tests
test_files=$(printf '%s\n%s' "$changed_files" "$staged_files" \
  | sort -u \
  | grep -E 'tests?/|\.Tests\.|\.spec\.|\.test\.' \
  || true)

if [ -n "$test_files" ]; then
  context+=(
    "Des tests ont été modifiés ou ajoutés :"
    "$test_files"
    "Vérifie qu'ils sont déterministes, isolés et qu'ils valident le comportement observable."
  )
fi

# 8. Vérification des composants Blazor
blazor_files=$(printf '%s\n%s' "$changed_files" "$staged_files" \
  | sort -u \
  | grep -E '\.razor$' \
  || true)

if [ -n "$blazor_files" ]; then
  context+=(
    "Des composants Blazor ont été modifiés :"
    "$blazor_files"
    "Vérifie la séparation UI / logique métier, les états de chargement, d'erreur et vide,"
    "l'accessibilité, la libération des ressources et les tests bUnit."
  )
fi

# 9. Vérification de la CI/CD
ci_files=$(printf '%s\n%s' "$changed_files" "$staged_files" \
  | sort -u \
  | grep -E '\.github/workflows/|Dockerfile|docker-compose|kubernetes|terraform|helm' \
  || true)

if [ -n "$ci_files" ]; then
  context+=(
    "ALERTE : des fichiers CI/CD ou d'infrastructure ont été modifiés :"
    "$ci_files"
    "Ne déploie rien et ne fusionne rien sans validation humaine explicite."
    "Vérifie les secrets, les permissions, les environnements et le rollback."
  )
fi

# 10. Vérification de la dernière réponse
if [ -n "$final_message" ]; then
  if printf '%s' "$final_message" | grep -qiE 'secret|token|password|api key|connection string'; then
    context+=(
      "ALERTE : la dernière réponse peut contenir une donnée sensible."
      "Ne la copie pas dans un fichier, un log, une PR ou un message utilisateur."
      "Masque immédiatement toute valeur sensible."
    )
  fi
fi

# 11. Recommandations finales
context+=(
  "Avant de terminer :"
  "- Vérifie que le build et les tests pertinents ont été exécutés."
  "- Vérifie qu'aucun secret n'est exposé."
  "- Vérifie qu'aucune migration n'a été appliquée sans validation."
  "- Vérifie qu'aucun fichier CI/CD ou d'infrastructure n'a été modifié sans validation."
  "- Crée une PR claire si des modifications doivent être livrées."
)

# 12. Format JSON
json_context=$(printf '%s\n' "${context[@]}" | jq -Rs .)
printf '{"additionalContext":%s}\n' "$json_context"