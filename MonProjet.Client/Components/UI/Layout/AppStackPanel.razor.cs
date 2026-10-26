using Microsoft.AspNetCore.Components;
using MonProjet.Client.Components.UI.Core;

namespace MonProjet.Client.Components.UI.Layout;

public partial class AppStackPanel : AppComponentBase
{
    [Parameter] public StackOrientation Orientation { get; set; } = StackOrientation.Vertical;
    [Parameter] public int Gap { get; set; } = 8;

    // Alignement principal (axe de l'empilement)
    [Parameter] public StackAlignment MainAxisAlignment { get; set; } = StackAlignment.Start;

    // Alignement croisé (perpendiculaire à l'empilement)
    [Parameter] public StackAlignment CrossAxisAlignment { get; set; } = StackAlignment.Stretch;

    [Parameter] public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Calcule les styles CSS Flexbox pour le conteneur interne.
    /// </summary>
    public string FinalFlexStyles
    {
        get
        {
            var flexDirection = Orientation == StackOrientation.Vertical ? "column" : "row";

            var styles = new List<string>
            {
                "display: flex",
                $"flex-direction: {flexDirection}",
                $"gap: {Gap}px",
                $"justify-content: {MapAlignment(MainAxisAlignment)}",
                $"align-items: {MapAlignment(CrossAxisAlignment)}"
            };

            return string.Join("; ", styles) + ";";
        }
    }

    private static string MapAlignment(StackAlignment alignment) => alignment switch
    {
        StackAlignment.Start => "flex-start",
        StackAlignment.Center => "center",
        StackAlignment.End => "flex-end",
        StackAlignment.Stretch => "stretch",
        StackAlignment.SpaceBetween => "space-between",
        StackAlignment.SpaceAround => "space-around",
        StackAlignment.SpaceEvenly => "space-evenly",
        _ => "flex-start"
    };

    public enum StackOrientation { Vertical, Horizontal }

    public enum StackAlignment
    {
        Start,
        Center,
        End,
        Stretch,
        SpaceBetween,
        SpaceAround,
        SpaceEvenly
    }
}