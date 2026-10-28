using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace MonProjet.Client.Components.UI.Core;

public abstract class AppComponentBase : ComponentBase
{
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
        => string.Join(' ', classes.Where(c => !string.IsNullOrWhiteSpace(c)));

    /// <summary>
    /// Filtre les attributs générés par l'isolation CSS (qui commencent par 'b-')
    /// pour éviter l'erreur RZ10007.
    /// </summary>
    protected Dictionary<string, object> SafeAttributes
    {
        get
        {
            if (AdditionalAttributes == null) return [];

            return AdditionalAttributes
                .Where(a => !a.Key.StartsWith("b-", StringComparison.OrdinalIgnoreCase))
                .ToDictionary(a => a.Key, a => a.Value);
        }
    }
}