
```bash
#!/usr/bin/env bash
# Bloque toute écriture provenant de blazor-refactoring-agent.
# Defense-in-depth : même si l'outil edit/write est réintroduit par erreur.
set -euo pipefail

INPUT=$(cat)
AGENT=$(echo "$INPUT" | jq -r '.agent_name // empty')
TOOL=$(echo "$INPUT" | jq -r '.tool_name // empty')

# Matcher : écriture uniquement
if [[ "$TOOL" != "edit" && "$TOOL" != "write" ]]; then
  exit 0
fi

# Bloquer si l'agent est l'orchestrateur
if [[ "$AGENT" == "blazor-refactoring-agent" ]]; then
  jq -n '{
    permissionDecision: "deny",
    permissionDecisionReason: "blazor-refactoring-agent est un orchestrateur : délègue à blazor-refactor-executor."
  }'
  exit 0
fi

exit 0