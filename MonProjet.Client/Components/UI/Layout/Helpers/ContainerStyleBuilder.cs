using MonProjet.Client.Components.UI.Layout.Enums;

namespace MonProjet.Client.Components.UI.Layout.Helpers;

public static class ContainerStyleBuilder
{
    public static string BuildOuterStyles(
        ContainerAlignment horizontal,
        ContainerAlignment vertical,
        string? width, string? height,
        string? marginLeft, string? marginRight, string? marginTop, string? marginBottom,
        bool isEnabled)
    {
        var styles = new List<string>
        {
            "display: flex",
            "flex-direction: row",
            $"justify-content: {MapAlignment(horizontal)}",
            $"align-items: {MapAlignment(vertical)}",
            "box-sizing: border-box"
        };

        if (!string.IsNullOrEmpty(width)) styles.Add($"width: {width}");
        if (!string.IsNullOrEmpty(height)) styles.Add($"height: {height}");

        if (!string.IsNullOrEmpty(marginLeft)) styles.Add($"margin-left: {marginLeft}");
        if (!string.IsNullOrEmpty(marginRight)) styles.Add($"margin-right: {marginRight}");
        if (!string.IsNullOrEmpty(marginTop)) styles.Add($"margin-top: {marginTop}");
        if (!string.IsNullOrEmpty(marginBottom)) styles.Add($"margin-bottom: {marginBottom}");

        if (!isEnabled)
        {
            styles.Add("opacity: 0.6");
            styles.Add("pointer-events: none");
        }

        return string.Join("; ", styles) + ";";
    }

    public static string BuildInnerStyles(
        string? paddingLeft, string? paddingRight, string? paddingTop, string? paddingBottom)
    {
        var styles = new List<string> { "width: 100%", "height: 100%", "box-sizing: border-box" };

        if (!string.IsNullOrEmpty(paddingLeft)) styles.Add($"padding-left: {paddingLeft}");
        if (!string.IsNullOrEmpty(paddingRight)) styles.Add($"padding-right: {paddingRight}");
        if (!string.IsNullOrEmpty(paddingTop)) styles.Add($"padding-top: {paddingTop}");
        if (!string.IsNullOrEmpty(paddingBottom)) styles.Add($"padding-bottom: {paddingBottom}");

        return string.Join("; ", styles) + ";";
    }

    private static string MapAlignment(ContainerAlignment alignment) => alignment switch
    {
        ContainerAlignment.Start => "flex-start",
        ContainerAlignment.Center => "center",
        ContainerAlignment.End => "flex-end",
        ContainerAlignment.Stretch => "stretch",
        ContainerAlignment.SpaceBetween => "space-between",
        ContainerAlignment.SpaceAround => "space-around",
        ContainerAlignment.SpaceEvenly => "space-evenly",
        _ => "flex-start"
    };
}