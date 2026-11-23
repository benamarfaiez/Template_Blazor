$ErrorActionPreference = 'Stop'

function EmitDecision {
    param([string]$Decision, [string]$Reason = '')
    $response = @{ permissionDecision = $Decision }
    if ($Reason) {
        $response['permissionDecisionReason'] = $Reason
    }
    Write-Output ($response | ConvertTo-Json -Depth 3 -Compress)
    exit 0
}

$inputRaw = [Console]::In.ReadToEnd()

if ($inputRaw.Length -gt 1048576) {
    EmitDecision -Decision 'deny' -Reason 'Garde orchestrateur : input trop volumineux.'
}

try {
    $payload = $inputRaw | ConvertFrom-Json -ErrorAction Stop
} catch {
    EmitDecision -Decision 'deny' -Reason 'Garde orchestrateur : input JSON invalide.'
}

$toolName = $payload.toolName
if (-not $toolName) {
    $toolName = $payload.tool_name
}

$agentName = $payload.agentName
if (-not $agentName) {
    $agentName = $payload.agent_name
}
if (-not $agentName) {
    $agentName = $payload.agentType
}
if (-not $agentName) {
    $agentName = $payload.agent_type
}
if (-not $agentName) {
    $agentName = $payload.role
}
if (-not $agentName -and $payload.agent) {
    if ($payload.agent -is [string]) {
        $agentName = $payload.agent
    } elseif ($payload.agent.name) {
        $agentName = $payload.agent.name
    }
}
if (-not $agentName -and $payload.context) {
    $agentName = $payload.context.agentName
}
if (-not $agentName) {
    $agentName = if ($env:COPILOT_AGENT_NAME) {
        $env:COPILOT_AGENT_NAME
    } elseif ($env:COPILOT_AGENT) {
        $env:COPILOT_AGENT
    } elseif ($env:GH_COPILOT_AGENT) {
        $env:GH_COPILOT_AGENT
    } else {
        ''
    }
}

if ($agentName -notmatch '(?i)orchestrator|blazor-refactoring-agent') {
    EmitDecision -Decision 'allow'
}

if ($toolName -in @('write', 'edit', 'create', 'delete', 'remove', 'move', 'rename')) {
    EmitDecision -Decision 'deny' -Reason 'La garde orchestrateur interdit les écritures directes. Déléguer le changement à blazor-refactor-executor.'
}

$toolCommand = $null
if ($payload.toolArgs -and $payload.toolArgs.PSObject.Properties['command']) {
    $toolCommand = $payload.toolArgs.command
}
if (-not $toolCommand -and $payload.toolArgs -and $payload.toolArgs.PSObject.Properties['cmd']) {
    $toolCommand = $payload.toolArgs.cmd
}
if (-not $toolCommand) {
    $toolCommand = $payload.command
}

if ($toolName -in @('bash', 'shell', 'powershell')) {
    $mutationPattern = '(?i)(^|\s|[;&|])(git\s+(apply|add|commit|merge|rebase|reset|checkout|clean)\b|apply_patch\b|cp\b|copy\b|move\b|mv\b|rm\b|remove-item\b|del\b|mkdir\b|touch\b|tee\b)|>>?\s*\S+'
    if ($toolCommand -match $mutationPattern) {
        EmitDecision -Decision 'deny' -Reason 'La garde orchestrateur interdit les modifications de fichiers directes. Déléguer à blazor-refactor-executor.'
    }
}

EmitDecision -Decision 'allow'
