using Microsoft.AspNetCore.Components;
using MonProjet.Client.Components.UI.Core;

namespace MonProjet.Client.Components.UI.Layout;

public partial class AppContainer : AppComponentBase
{
    [Parameter] public string HorizontalAlignment { get; set; } = "stretch";
    [Parameter] public string VerticalAlignment { get; set; } = "stretch";

    [Parameter] public bool IsEnabled { get; set; } = true;
    [Parameter] public RenderFragment? ChildContent { get; set; }

    public string FinalStyles { get; private set; } = string.Empty;
    public string? FinalCssClasses { get; private set; } = string.Empty;

    public bool IsActuallyEnabled => IsEnabled && IsVisible;

    protected override void OnParametersSet()
    {
        BuildStyleAttribute();
        base.OnParametersSet();
    }

    private void BuildStyleAttribute()
    {
        // 1. Gestion de la visibilité
        if (!IsVisible)
        {
            FinalStyles = "display: none;";
            FinalCssClasses = "app-container";
            return;
        }

        // 2. Styles du conteneur extérieur (Layout Flexbox et Marges)
        var outerStyles = new List<string>
        {
            "display: flex",
            "flex-direction: row",
            $"justify-content: {MapAlignment(HorizontalAlignment, "flex-start")}",
            $"align-items: {MapAlignment(VerticalAlignment, "flex-start")}"
        };

        if (!string.IsNullOrEmpty(Width)) outerStyles.Add($"width: {Width}");
        if (!string.IsNullOrEmpty(Height)) outerStyles.Add($"height: {Height}");

        if (!string.IsNullOrEmpty(MarginLeft)) outerStyles.Add($"margin-left: {MarginLeft}");
        if (!string.IsNullOrEmpty(MarginRight)) outerStyles.Add($"margin-right: {MarginRight}");
        if (!string.IsNullOrEmpty(MarginTop)) outerStyles.Add($"margin-top: {MarginTop}");
        if (!string.IsNullOrEmpty(MarginBottom)) outerStyles.Add($"margin-bottom: {MarginBottom}");

        // Gestion de l'état désactivé
        if (!IsEnabled)
        {
            outerStyles.Add("opacity: 0.6");
            outerStyles.Add("pointer-events: none"); // Désactive les clics
        }

        FinalStyles = string.Join("; ", outerStyles) + ";";

        var innerStyles = new List<string>();

        if (!string.IsNullOrEmpty(PaddingLeft)) innerStyles.Add($"padding-left: {PaddingLeft}");
        if (!string.IsNullOrEmpty(PaddingRight)) innerStyles.Add($"padding-right: {PaddingRight}");
        if (!string.IsNullOrEmpty(PaddingTop)) innerStyles.Add($"padding-top: {PaddingTop}");
        if (!string.IsNullOrEmpty(PaddingBottom)) innerStyles.Add($"padding-bottom: {PaddingBottom}");

        FinalStyles += string.Join("; ", innerStyles) + ";";
        // 4. Classes CSS
        FinalCssClasses = ClassNames("app-container", AdditionalAttributes?["class"]?.ToString());
    }

    private static string MapAlignment(string align, string defaultVal)
    {
        return align?.ToLowerInvariant() switch
        {
            "start" or "left" or "top" => "flex-start",
            "center" => "center",
            "end" or "right" or "bottom" => "flex-end",
            "stretch" => "stretch",
            "space-between" => "space-between",
            "space-around" => "space-around",
            "space-evenly" => "space-evenly",
            _ => defaultVal
        };
    }
}