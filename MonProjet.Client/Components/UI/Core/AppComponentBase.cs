using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace MonProjet.Client.Components.UI.Core;

public abstract class AppComponentBase : ComponentBase
{
    /// <summary>
    /// Instance JSInterop injectée, disponible dans tous les composants dérivés.
    /// Permet l'import de modules JS (IJSObjectReference) via JSRuntime.InvokeAsync.
    /// </summary>
    [Inject] protected IJSRuntime JSRuntime { get; set; } = default!;

    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    protected static string? ClassNames(params string?[] classes)
    {
        return string.Join(' ', classes.Where(c => !string.IsNullOrWhiteSpace(c)));
    }
}