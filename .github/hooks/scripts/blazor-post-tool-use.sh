#!/usr/bin/env bash
set -euo pipefail

input=$(cat)
tool_name=$(printf '%s' "$input" | jq -r '.toolName // empty')
tool_args=$(printf '%s' "$input" | jq -r '.toolArgs // empty')
tool_error=$(printf '%s' "$input" | jq -r '.error // empty')

# 1. Après une édition de code
if [ "$tool_name" = "edit" ] || [ "$tool_name" = "write" ]; then
  changed_files=$(git diff --name-only --diff-filter=ACMR 2>/dev/null || true)

  context=()

  if printf '%s' "$changed_files" | grep -qE '\.(cs|razor)$'; then
    context+=(
      "Un fichier C# ou Razor a été modifié."
      "Avant de proposer une PR :"
      "- Exécute dotnet build --no-restore."
      "- Exécute les tests pertinents avec dotnet test --no-build."
      "- Vérifie le formatage avec dotnet format --verify-no-changes."
      "- Pour un composant Razor, vérifie les états de chargement, vide, erreur et succès."
      "- Pour un service, vérifie la validation, les erreurs métier et les cas limites."
    )
  fi

  if printf '%s' "$changed_files" | grep -qE '\.razor$'; then
    context+=(
      "Un composant Blazor a été modifié."
      "Vérifie :"
      "- La séparation UI / logique métier."
      "- Les paramètres immuables et EventCallback<T>."
      "- La libération des ressources avec IDisposable ou IAsyncDisposable."
      "- La propagation de CancellationToken pour les opérations longues."
      "- L'accessibilité : labels, navigation clavier, focus et messages d'erreur."
      "- Les tests bUnit couvrant le rendu et les interactions."
    )
  fi

  if printf '%s' "$changed_files" | grep -qE '\.csproj$'; then
    context+=(
      "Un fichier projet .csproj a été modifié."
      "Vérifie :"
      "- La nécessité réelle de la dépendance ajoutée."
      "- Les vulnérabilités avec dotnet list package --vulnerable --include-transitive."
      "- La compatibilité avec la version .NET du projet."
      "- L'impact sur le bundle et le temps de démarrage en Blazor WebAssembly."
    )
  fi

  if printf '%s' "$changed_files" | grep -qE 'appsettings.*\.json|\.env|\.yml|\.yaml'; then
    context+=(
      "Un fichier de configuration a été modifié."
      "Vérifie :"
      "- Aucun secret, token, mot de passe ou chaîne de connexion n'est exposé."
      "- Les valeurs sensibles proviennent de variables d'environnement ou d'un gestionnaire de secrets."
      "- Les changements sont documentés et compatibles avec les environnements de déploiement."
    )
  fi

  if printf '%s' "$changed_files" | grep -qE 'Migrations/|\.edmx'; then
    context+=(
      "Une migration ou un modèle de base de données a été modifié."
      "ALERTE : une migration ne doit jamais être appliquée automatiquement."
      "Demande une validation humaine avant dotnet ef database update."
      "Vérifie la réversibilité, les données existantes et le plan de rollback."
    )
  fi

  if printf '%s' "$changed_files" | grep -qE 'tests?/|\.Tests\.|\.spec\.|\.test\.'; then
    context+=(
      "Un test a été modifié ou ajouté."
      "Exécute les tests ciblés et vérifie qu'ils valident le comportement observable,"
      "pas les détails d'implémentation."
    )
  fi

  if printf '%s' "$changed_files" | grep -qE '\.github/workflows/|Dockerfile|docker-compose|kubernetes|terraform'; then
    context+=(
      "ALERTE : un fichier CI/CD ou d'infrastructure a été modifié."
      "Ne déploie rien et ne fusionne rien sans validation humaine explicite."
      "Vérifie les secrets, les permissions, les environnements et le rollback."
    )
  fi

  if [ -n "$tool_error" ]; then
    context+=(
      "L'outil a retourné une erreur."
      "Ne relance pas aveuglément la même action."
      "Analyse la cause racine et demande une clarification si nécessaire."
    )
  fi

  if [ ${#context[@]} -eq 0 ]; then
    printf '%s\n' '{}'
    exit 0
  fi

  json_context=$(printf '%s\n' "${context[@]}" | jq -Rs .)
  printf '{"additionalContext":%s}\n' "$json_context"
  exit 0
fi

# 2. Après une commande shell
if [ "$tool_name" = "bash" ] || [ "$tool_name" = "shell" ]; then
  context=()

  if printf '%s' "$tool_args" | grep -qiE 'dotnet build'; then
    if [ -n "$tool_error" ]; then
      context+=(
        "Le build a échoué."
        "- Analyse les premières erreurs du compilateur."
        "- Ne désactive pas une règle d'analyse ou un test pour masquer l'échec."
        "- Corrige la cause racine ou demande une clarification."
      )
    else
      context+=(
        "Le build a réussi."
        "- Exécute maintenant les tests pertinents avec dotnet test --no-build."
        "- Vérifie le formatage avec dotnet format --verify-no-changes."
      )
    fi
  fi

  if printf '%s' "$tool_args" | grep -qiE 'dotnet test'; then
    if [ -n "$tool_error" ]; then
      context+=(
        "Des tests ont échoué."
        "- Classe chaque échec : bug existant, régression ou problème de test."
        "- Ne supprime ou ne désactive pas un test pour faire passer la suite."
        "- Corrige la cause racine ou demande une clarification."
      )
    else
      context+=(
        "Les tests ont réussi."
        "- Vérifie que les tests couvrent le comportement modifié."
        "- Pour un refactoring, vérifie que des tests de caractérisation existent."
      )
    fi
  fi

  if printf '%s' "$tool_args" | grep -qiE 'dotnet list package'; then
    if [ -n "$tool_error" ]; then
      context+=(
        "La vérification des packages a échoué."
        "- Analyse les vulnérabilités ou erreurs signalées."
        "- Ne mets pas à jour une dépendance majeure sans validation humaine."
      )
    fi
  fi

  if [ ${#context[@]} -eq 0 ]; then
    printf '%s\n' '{}'
    exit 0
  fi

  json_context=$(printf '%s\n' "${context[@]}" | jq -Rs .)
  printf '{"additionalContext":%s}\n' "$json_context"
  exit 0
fi

# Par défaut : ne rien ajouter
printf '%s\n' '{}'