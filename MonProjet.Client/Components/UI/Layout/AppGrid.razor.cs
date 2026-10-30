using Microsoft.AspNetCore.Components;
using MonProjet.Client.Components.UI.Core;

namespace MonProjet.Client.Components.UI.Layout;

public partial class AppGrid : AppComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Nombre de colonnes sur grand écran (Desktop).
    /// </summary>
    [Parameter] public int Columns { get; set; } = 2;

    /// <summary>
    /// Espacement entre les éléments (ex: "16px", "1rem").
    /// </summary>
    [Parameter] public string Gap { get; set; } = "16px";

    /// <summary>
    /// Aligne verticalement le contenu des cellules (Start, Center, End, Stretch).
    /// </summary>
    [Parameter] public string AlignItems { get; set; } = "stretch";

    private string GridVariables => string.Format(
        System.Globalization.CultureInfo.InvariantCulture,
        "--grid-columns: {0}; --grid-gap: {1}; --grid-align: {2};",
        Columns, Gap, AlignItems);
}