using MonProjet.Client.Components.UI.Enums;

namespace MonProjet.Client.Components.UI.Helpers;

public static class FlexStyleBuilder
{
    public static string Build(StackOrientation orientation, int gap, StackAlignment mainAlign, StackAlignment crossAlign)
    {
        var flexDirection = orientation == StackOrientation.Vertical ? "column" : "row";

        var styles = new List<string>
        {
            "display: flex",
            $"flex-direction: {flexDirection}",
            $"gap: {gap}px",
            $"justify-content: {MapAlignment(mainAlign)}",
            $"align-items: {MapAlignment(crossAlign)}"
        };

        return string.Join("; ", styles) + ";";
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
}