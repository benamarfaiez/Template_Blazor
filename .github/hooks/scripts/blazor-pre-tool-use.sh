#!/usr/bin/env bash
# Hook PreToolUse — garde-fous déterministes pour agents Blazor.
# Retourne toujours un JSON {permissionDecision: allow|deny|ask}.
# Fail-closed : toute erreur de parsing = deny.
set -uo pipefail

# ---------------------------------------------------------------
# Utilitaires
# ---------------------------------------------------------------
emit() {
  local decision="$1" reason="${2:-}"
  if [ -z "$reason" ]; then
    printf '{"permissionDecision":"%s"}\n' "$decision"
  else
    local esc
    esc=$(printf '%s' "$reason" | jq -Rs .)
    printf '{"permissionDecision":"%s","permissionDecisionReason":%s}\n' \
      "$decision" "$esc"
  fi
  exit 0
}

log() { printf '[pre-tool-use] %s\n' "$*" >&2; }

# ---------------------------------------------------------------
# Lecture et validation de l'input
# ---------------------------------------------------------------
input=$(cat)

# B15 : borner la taille (1 Mo)
if [ "${#input}" -gt 1048576 ]; then
  emit "deny" "Hook PreToolUse : input trop volumineux (> 1 Mo)."
fi

# B4/B16 : valider le JSON
if ! printf '%s' "$input" | jq -e . >/dev/null 2>&1; then
  emit "deny" "Hook PreToolUse : input JSON invalide."
fi

tool_name=$(printf '%s' "$input" | jq -r '.toolName // empty')
if [ -z "$tool_name" ]; then
  emit "deny" "Hook PreToolUse : toolName absent."
fi

# B14 : extraire toutes les chaînes de toolArgs (chemins, commandes, etc.)
# `.. | strings` parcourt récursivement et retourne toutes les valeurs string.
tool_args_strings=$(printf '%s' "$input" \
  | jq -r '.toolArgs // {} | .. | strings' 2>/dev/null || true)

log "tool=$tool_name"
log "args_strings=$tool_args_strings"

# ---------------------------------------------------------------
# 1. Outils de lecture : allow
# ---------------------------------------------------------------
case "$tool_name" in
  view|read|search|glob|grep|list_files|get_file_contents|inspect)
    emit "allow"
    ;;
esac

# ---------------------------------------------------------------
# 2. Outils shell
# ---------------------------------------------------------------
if [ "$tool_name" = "bash" ] || [ "$tool_name" = "shell" ]; then

  # 2a. Patterns strictement destructifs → deny (B10, B11)
  blocked_patterns=(
    'dotnet ef database drop'
    'dotnet ef migrations remove'
    'git push --force'
    'git push -f'
    'git push --force-with-lease'
    'git reset --hard'
    'git clean -fd'
    'git clean -fdx'
    'git clean -x'
    'git checkout -- .'
    'git branch -D'
    'rm -rf'
    'rm -fr'
    'rm -r -f'
    'rm --recursive --force'
    'rmdir /s /q'
    'del /s /q'
    'Remove-Item -Recurse -Force'
    'shutdown'
    'reboot'
    'mkfs'
    'dd if='
    'chmod 777'
    'kubectl delete'
    'docker system prune'
    'docker volume rm'
    'helm uninstall'
    'terraform destroy'
    'az group delete'
  )

  for pattern in "${blocked_patterns[@]}"; do
    if printf '%s' "$tool_args_strings" | grep -qiF -- "$pattern"; then
      emit "deny" "Commande potentiellement destructive bloquée : $pattern."
    fi
  done

  # 2b. Installation / script externe → ask
  if printf '%s' "$tool_args_strings" | grep -qiE \
    'curl .*\|\s*(ba)?sh|wget .*\|\s*(ba)?sh|npm install|pnpm add|yarn add|dotnet tool install|dotnet add package'; then
    emit "ask" "Installation de dépendance ou script externe. Validation humaine requise."
  fi

  # 2c. Commandes build/test/lecture Git → allow (B2, B9)
  if printf '%s' "$tool_args_strings" | grep -qiE \
    'dotnet (build|test|format|list package|restore|run|publish|watch|ef dbcontext script)|git (status|diff|log|show|branch|remote|config --get|rev-parse|ls-files|blame|describe|reflog|stash list|tag)(\s|$)'; then
    # rejeter les variantes destructrices de git
    if ! printf '%s' "$tool_args_strings" | grep -qiE \
      'git (branch\s+(-d|-D|-m|-M|-f|-c)|tag\s+(-d|-f)|remote\s+(add|remove|rename|set-url|prune)|stash\s+(drop|clear|pop)|push|reset|checkout|clean|commit|merge|rebase)'; then
      emit "allow"
    fi
  fi

  # 2d. Fallback shell
  emit "ask" "Commande shell non classée. Vérifie son impact avant exécution."
fi

# ---------------------------------------------------------------
# 3. Fichiers sensibles → ask (B5)
# ---------------------------------------------------------------
if printf '%s' "$tool_args_strings" | grep -qiE \
  '(^|/)(\.env(\..*)?$|secrets?\.(json|ya?ml)$|appsettings\.(Production|Staging)\.json$|.*\.(pfx|p12|key|pem)$|credentials?\.(json|ya?ml)$)'; then
  emit "ask" "Fichier potentiellement sensible. Validation humaine requise."
fi

# ---------------------------------------------------------------
# 4-7. Protections par type de chemin, uniquement sur opération modifiante (B12)
# ---------------------------------------------------------------
case "$tool_name" in
  write|edit|create|delete|remove|move|rename)

    # 4. Migrations / DB (B6)
    if printf '%s' "$tool_args_strings" | grep -qiE \
      '(^|/)(Migrations?/|.*\.edmx$|appsettings\.json$)'; then
      emit "ask" "Modification d'une migration, d'un schéma de base ou d'une configuration. Validation humaine requise."
    fi

    # 5. Suppression de tests → deny (B7)
    if [ "$tool_name" = "delete" ] || [ "$tool_name" = "remove" ]; then
      if printf '%s' "$tool_args_strings" | grep -qiE \
        '(^|/)(tests?/|.*\.Tests\.|.*\.spec\.|.*\.test\.)'; then
        emit "deny" "Suppression de test bloquée. Validation humaine explicite requise."
      fi
    fi

    # 6. CI/CD et infrastructure
    if printf '%s' "$tool_args_strings" | grep -qiE \
      '(^|/)(\.github/(workflows|actions)/|Dockerfile$|docker-compose.*\.ya?ml$|.*\.(tf|tfvars)$|Chart\.yaml$|Jenkinsfile$|azure-pipelines\.ya?ml$)'; then
      emit "ask" "Modification de CI/CD ou d'infrastructure. Validation humaine requise."
    fi

    # 7. Composant Blazor (B3) — suppression → ask
    if [ "$tool_name" = "delete" ] || [ "$tool_name" = "remove" ]; then
      if printf '%s' "$tool_args_strings" | grep -qiE '\.razor$'; then
        emit "ask" "Suppression d'un composant Blazor. Vérifie références, routes et tests."
      fi
    fi
    ;;
esac

# ---------------------------------------------------------------
# 8. Fallback : allow (l'agent peut toujours lire/parcourir)
# ---------------------------------------------------------------
emit "allow"