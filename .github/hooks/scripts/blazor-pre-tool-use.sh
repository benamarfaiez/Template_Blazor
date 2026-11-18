#!/usr/bin/env bash
set -euo pipefail

input=$(cat)
tool_name=$(printf '%s' "$input" | jq -r '.toolName // empty')
tool_args=$(printf '%s' "$input" | jq -r '.toolArgs // empty')

# Autoriser explicitement les outils de lecture courants
case "$tool_name" in
  view|read|search|glob|grep|list_files|get_file_contents)
    printf '%s\n' '{"permissionDecision":"allow"}'
    exit 0
    ;;
esac

# 1. Commandes shell dangereuses
if [ "$tool_name" = "bash" ] || [ "$tool_name" = "shell" ]; then
  blocked_patterns=(
    'dotnet ef database drop'
    'dotnet ef database update'
    'dotnet ef migrations remove'
    'dotnet ef migrations add'
    'git push --force'
    'git push -f'
    'git reset --hard'
    'git clean -fd'
    'git clean -fdx'
    'rm -rf'
    'rmdir /s /q'
    'del /s /q'
    'Remove-Item -Recurse -Force'
    'kubectl delete'
    'docker system prune'
    'docker volume rm'
    'helm uninstall'
    'terraform destroy'
    'az group delete'
  )

  for pattern in "${blocked_patterns[@]}"; do
    if printf '%s' "$tool_args" | grep -qiF "$pattern"; then
      printf '%s\n' "{\"permissionDecision\":\"deny\",\"permissionDecisionReason\":\"Commande potentiellement destructive bloquée : $pattern. Demande une validation humaine explicite.\"}"
      exit 0
    fi
  done

  # Bloquer les commandes réseau ou d'installation non validées
  if printf '%s' "$tool_args" | grep -qiE \
    'curl .*\| *(ba)?sh|wget .*\| *(ba)?sh|npm install|pnpm add|yarn add|dotnet tool install|dotnet add package'; then
    printf '%s\n' '{"permissionDecision":"ask","permissionDecisionReason":"Installation de dépendance ou exécution de script externe détectée. Validation humaine requise."}'
    exit 0
  fi

  # Autoriser les commandes de build et de test
  if printf '%s' "$tool_args" | grep -qiE \
    'dotnet build|dotnet test|dotnet format|dotnet list package|dotnet ef dbcontext script'; then
    printf '%s\n' '{"permissionDecision":"allow"}'
    exit 0
  fi

  # Demander confirmation pour les autres commandes shell
  printf '%s\n' '{"permissionDecision":"ask","permissionDecisionReason":"Commande shell non classée. Vérifie son impact avant exécution."}'
  exit 0
fi

# 2. Protection des fichiers sensibles
if printf '%s' "$tool_args" | grep -qiE \
  'appsettings\.Production\.json|\.env|\.pfx|\.key|secret|credential|password|connectionstring'; then
  printf '%s\n' '{"permissionDecision":"ask","permissionDecisionReason":"Fichier potentiellement sensible détecté. Validation humaine requise avant lecture ou modification."}'
  exit 0
fi

# 3. Protection des migrations et de la base de données
if printf '%s' "$tool_args" | grep -qiE 'Migrations/|migrations|\.edmx|database|connectionstring'; then
  if printf '%s' "$tool_args" | grep -qiE 'delete|remove|drop|update|write|edit'; then
    printf '%s\n' '{"permissionDecision":"ask","permissionDecisionReason":"Modification potentielle d’une migration, d’un schéma de base ou d’une configuration de connexion. Validation humaine requise."}'
    exit 0
  fi
fi

# 4. Protection des tests
if printf '%s' "$tool_args" | grep -qiE 'tests?/|\.Tests\.|\.spec\.|\.test\.'; then
  if printf '%s' "$tool_args" | grep -qiE 'delete|remove'; then
    printf '%s\n' '{"permissionDecision":"deny","permissionDecisionReason":"Suppression de test bloquée. Les tests ne doivent pas être supprimés sans validation humaine explicite."}'
    exit 0
  fi
fi

# 5. Protection des fichiers CI/CD et infrastructure
if printf '%s' "$tool_args" | grep -qiE \
  '\.github/workflows/|\.github/actions/|Dockerfile|docker-compose|kubernetes|k8s|terraform|helm|\.azuredevops|Jenkinsfile'; then
  printf '%s\n' '{"permissionDecision":"ask","permissionDecisionReason":"Modification de CI/CD ou d’infrastructure détectée. Validation humaine requise."}'
  exit 0
fi

# 6. Protection des fichiers Razor critiques
if printf '%s' "$tool_args" | grep -qiE '\.razor$'; then
  if printf '%s' "$tool_args" | grep -qiE 'delete|remove'; then
    printf '%s\n' '{"permissionDecision":"ask","permissionDecisionReason":"Suppression d’un composant Blazor détectée. Vérifie les références, routes et tests avant validation."}'
    exit 0
  fi
fi

# Par défaut : autoriser
printf '%s\n' '{"permissionDecision":"allow"}'