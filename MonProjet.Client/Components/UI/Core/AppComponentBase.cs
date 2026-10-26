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

    [Parameter] public string? Width { get; set; }
    [Parameter] public string? Height { get; set; }
    [Parameter] public string? MarginLeft { get; set; }
    [Parameter] public string? MarginRight { get; set; }
    [Parameter] public string? MarginTop { get; set; }
    [Parameter] public string? MarginBottom { get; set; }
    [Parameter] public string? PaddingLeft { get; set; }
    [Parameter] public string? PaddingRight { get; set; }
    [Parameter] public string? PaddingTop { get; set; }
    [Parameter] public string? PaddingBottom { get; set; }
    [Parameter] public bool IsVisible { get; set; } = true;

    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    protected static string? ClassNames(params string?[] classes)
    {
        return string.Join(' ', classes.Where(c => !string.IsNullOrWhiteSpace(c)));
    }
}