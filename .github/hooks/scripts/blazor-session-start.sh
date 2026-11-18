#!/usr/bin/env bash
set -euo pipefail

context=()

# 1. Vérifier le SDK .NET
if ! command -v dotnet >/dev/null 2>&1; then
  context+=(
    "ALERTE : le SDK .NET n'est pas disponible dans cet environnement."
    "Ne lance pas de build ou de test tant que l'environnement n'est pas corrigé."
  )
else
  dotnet_version="$(dotnet --version 2>/dev/null || echo 'inconnue')"
  context+=("SDK .NET détecté : $dotnet_version.")
fi

# 2. Détecter la solution
solution_files=$(find . -maxdepth 2 -type f -name '*.sln' 2>/dev/null || true)

if [ -n "$solution_files" ]; then
  context+=(
    "Solutions détectées :"
    "$solution_files"
    "Utilise la solution appropriée pour le build et les tests."
  )
else
  context+=(
    "ALERTE : aucun fichier .sln détecté à moins de deux niveaux de profondeur."
    "Identifie les projets .csproj avant de lancer un build ou des tests."
  )
fi

# 3. Détecter les projets Blazor
blazor_projects=$(find . -type f -name '*.csproj' \
  -not -path '*/bin/*' \
  -not -path '*/obj/*' \
  -exec grep -lE 'Microsoft.AspNetCore.Components|Microsoft.AspNetCore.Components.WebAssembly|Microsoft.AspNetCore.Components.WebView' {} \; 2>/dev/null || true)

if [ -n "$blazor_projects" ]; then
  context+=(
    "Projets Blazor détectés :"
    "$blazor_projects"
  )

  if printf '%s' "$blazor_projects" | grep -q 'WebAssembly'; then
    context+=("Variante Blazor probable : WebAssembly.")
  elif printf '%s' "$blazor_projects" | grep -q 'WebView'; then
    context+=("Variante Blazor probable : Hybrid.")
  else
    context+=("Variante Blazor probable : Server ou Auto. Confirme-la dans les fichiers projet.")
  fi
else
  context+=("ALERTE : aucun projet Blazor détecté. Confirme le type d'application avant de proposer des modifications.")
fi

# 4. Détecter les projets de tests
test_projects=$(find . -type f -name '*.csproj' \
  -not -path '*/bin/*' \
  -not -path '*/obj/*' \
  -exec grep -lE 'xunit|nunit|mstest|bunit|Microsoft.Playwright' {} \; 2>/dev/null || true)

if [ -n "$test_projects" ]; then
  context+=(
    "Projets de tests détectés :"
    "$test_projects"
  )
else
  context+=(
    "ALERTE : aucun projet de test détecté."
    "Pour tout refactoring, propose d'abord des tests de caractérisation."
  )
fi

# 5. Détecter les migrations EF Core
migration_files=$(find . -type d -name 'Migrations' \
  -not -path '*/bin/*' \
  -not -path '*/obj/*' 2>/dev/null || true)

if [ -n "$migration_files" ]; then
  context+=(
    "Dossiers de migrations EF Core détectés :"
    "$migration_files"
    "Ne lance jamais une migration, une suppression de base ou une suppression de migration sans validation humaine explicite."
  )
fi

# 6. Détecter les fichiers de secrets potentiels
secret_files=$(find . \
  -type f \( -name '*.env' -o -name '*.pfx' -o -name '*.key' -o -name '*secret*' \) \
  -not -path '*/bin/*' \
  -not -path '*/obj/*' \
  -not -path '*/.git/*' 2>/dev/null | head -n 20 || true)

if [ -n "$secret_files" ]; then
  context+=(
    "Fichiers potentiellement sensibles détectés :"
    "$secret_files"
    "Ne lis, ne copie et ne journalise jamais leur contenu."
  )
fi

# 7. Règles de sécurité Blazor
context+=(
  "Règles de sécurité Blazor :"
  "- Applique l'autorisation côté serveur, même si l'UI masque une action."
  "- Valide toute entrée utilisateur côté serveur."
  "- Ne stocke jamais de secret dans le code, les tests ou les fichiers versionnés."
  "- Utilise des requêtes paramétrées pour prévenir l'injection SQL."
  "- Isole le JavaScript interop derrière un service dédié."
)

# 8. Règles de refactoring
context+=(
  "Règles de refactoring :"
  "- Préserve le comportement utilisateur observable."
  "- Ajoute ou confirme les tests de caractérisation avant modification."
  "- Limite chaque PR à un périmètre réduit et réversible."
  "- Exécute dotnet build --no-restore et dotnet test --no-build lorsque possible."
  "- Ne mélange pas refactoring, nouvelle fonctionnalité, correction de bug et migration."
)

# 9. Commandes recommandées
context+=(
  "Commandes recommandées :"
  "- dotnet restore"
  "- dotnet build --no-restore"
  "- dotnet test --no-build"
  "- dotnet test --collect:\"XPlat Code Coverage\""
  "- dotnet format --verify-no-changes"
)

# 10. Format JSON pour additionalContext
json_context=$(printf '%s\n' "${context[@]}" | jq -Rs .)

printf '{"additionalContext":%s}\n' "$json_context"