using Microsoft.AspNetCore.Components;
using MonProjet.Client.Components.UI.Core;
using MonProjet.Client.Components.UI.Enums;

namespace MonProjet.Client.Components.UI.Feedback;

public partial class FontIcon : AppComponentBase
{
    /// <summary>
    /// Le caractère ou l'emoji à afficher (ex: "📁", "★").
    /// </summary>
    [Parameter] public string Icon { get; set; } = string.Empty;

    /// <summary>
    /// Taille de l'icône (ex: "1em", "24px").
    /// </summary>
    [Parameter] public string Size { get; set; } = "1em";

    /// <summary>
    /// Couleur de l'icône.
    /// </summary>
    [Parameter] public IconVariant Variant { get; set; } = IconVariant.Inherit;

    /// <summary>
    /// Si vrai, l'icône est ignorée par les lecteurs d'écran (si elle est purement décorative).
    /// Si faux, un aria-label doit être fourni.
    /// </summary>
    [Parameter] public bool IsDecorative { get; set; } = true;

    private string VariantClass => Variant != IconVariant.Inherit ? $"icon-{Variant.ToString().ToLowerInvariant()}" : "";

    /// <summary>
    /// Génère les styles inline (margin, padding, width) hérités de AppComponentBase.
    /// </summary>
    private string FinalStyles
    {
        get
        {
            var styles = new List<string> { $"font-size: {Size}" };

            if (!string.IsNullOrEmpty(Width)) styles.Add($"width: {Width}");
            if (!string.IsNullOrEmpty(Height)) styles.Add($"height: {Height}");

            if (!string.IsNullOrEmpty(MarginLeft)) styles.Add($"margin-left: {MarginLeft}");
            if (!string.IsNullOrEmpty(MarginRight)) styles.Add($"margin-right: {MarginRight}");
            if (!string.IsNullOrEmpty(MarginTop)) styles.Add($"margin-top: {MarginTop}");
            if (!string.IsNullOrEmpty(MarginBottom)) styles.Add($"margin-bottom: {MarginBottom}");

            if (!string.IsNullOrEmpty(PaddingLeft)) styles.Add($"padding-left: {PaddingLeft}");
            if (!string.IsNullOrEmpty(PaddingRight)) styles.Add($"padding-right: {PaddingRight}");
            if (!string.IsNullOrEmpty(PaddingTop)) styles.Add($"padding-top: {PaddingTop}");
            if (!string.IsNullOrEmpty(PaddingBottom)) styles.Add($"padding-bottom: {PaddingBottom}");

            return string.Join("; ", styles);
        }
    }
}