#!/usr/bin/env bash
set -euo pipefail

emit() {
  local decision="$1"
  local reason="${2:-}"
  if [ -n "$reason" ]; then
    local escaped
    escaped=$(printf '%s' "$reason" | jq -Rs .)
    printf '{"permissionDecision":"%s","permissionDecisionReason":%s}\n' "$decision" "$escaped"
  else
    printf '{"permissionDecision":"%s"}\n' "$decision"
  fi
  exit 0
}

input=$(cat)

if [ "${#input}" -gt 1048576 ]; then
  emit deny "Garde orchestrateur : input trop volumineux."
fi

if ! printf '%s' "$input" | jq -e . >/dev/null 2>&1; then
  emit deny "Garde orchestrateur : input JSON invalide."
fi

tool_name=$(printf '%s' "$input" | jq -r '.toolName // .tool_name // empty')
agent_name=$(printf '%s' "$input" | jq -r '
  .agentName // .agent_name // .agentName //
  .agentType // .agent_type // .role //
  .actor.name // .actor //
  .agent.name // .agent // .context.agentName //
  empty
')

if [ -z "$agent_name" ]; then
  agent_name="${COPILOT_AGENT_NAME:-${COPILOT_AGENT:-${GH_COPILOT_AGENT:-}}}"
fi

trusted_agent_name="${COPILOT_AGENT_NAME:-${COPILOT_AGENT:-${GH_COPILOT_AGENT:-}}}"
if [ -n "$trusted_agent_name" ]; then
  agent_name="$trusted_agent_name"
fi

if [ -n "$agent_name" ] && ! printf '%s' "$agent_name" | grep -qiE 'orchestrator|blazor-refactoring-agent'; then
  emit allow
fi

case "$tool_name" in
  write|edit|create|delete|remove|move|rename)
    emit deny "La garde orchestrateur interdit les écritures directes. Déléguer le changement à blazor-refactor-executor."
    ;;
esac

tool_command=$(printf '%s' "$input" | jq -r '
  .toolArgs.command // .toolArgs.cmd // .command // empty
')

if [ "$tool_name" = "bash" ] || [ "$tool_name" = "shell" ] || [ "$tool_name" = "powershell" ]; then
  if printf '%s' "$tool_command" | grep -qiE \
    '(^|[;&|[:space:]])(git[[:space:]]+(apply|add|commit|merge|rebase|reset|checkout|clean)|apply_patch|cp|copy|move|mv|rm|remove-item|del|mkdir|touch|tee)([[:space:]]|$)|>[[:space:]]*[^[:space:]]|>>[[:space:]]*[^[:space:]]'; then
    emit deny "La garde orchestrateur interdit les modifications de fichiers directes. Déléguer à blazor-refactor-executor."
  fi

  read_only_command_regex='^[[:blank:]]*(git[[:blank:]]+(status|diff|log|show|rev-parse|ls-files)([[:blank:]]+[^;&|<>`$(){}[:cntrl:]]+)*|git[[:blank:]]+(branch|tag)([[:blank:]]+--list)?|dotnet[[:blank:]]+(build|test|list[[:blank:]]+package)([[:blank:]]+[^;&|<>`$(){}[:cntrl:]]+)*|(Get-ChildItem|Get-Content|Get-Item|Get-Location|Select-String|Test-Path|cat|ls|pwd|head|tail|grep|rg|wc|stat|file)([[:blank:]]+[^;&|<>`$(){}[:cntrl:]]+)*)[[:blank:]]*$'
  if [[ ! "$tool_command" =~ [\;\&\|\<\>\`\$\(\)\{\}]] \
    && "$tool_command" =~ $read_only_command_regex ]]; then
    emit allow
  fi

  emit deny "Commande shell non vérifiée. Déléguer l’opération à blazor-refactor-executor."
fi

emit allow
