using Microsoft.AspNetCore.Components;
using MonProjet.Client.Components.UI.Core;
using MonProjet.Client.Components.UI.Enums;

namespace MonProjet.Client.Components.UI.Feedback;

public partial class AppProgressRing : AppComponentBase
{
    /// <summary>
    /// Taille du loader (ex: "32px", "2rem").
    /// </summary>
    [Parameter] public string Size { get; set; } = "32px";

    /// <summary>
    /// Épaisseur du trait (ex: "3px", "4px").
    /// </summary>
    [Parameter] public string Thickness { get; set; } = "3px";

    /// <summary>
    /// Couleur du loader.
    /// </summary>
    [Parameter] public ProgressRingVariant Variant { get; set; } = ProgressRingVariant.Primary;

    // Map la variante vers une classe CSS
    private string VariantClass => $"app-progressring-{Variant.ToString().ToLowerInvariant()}";
}