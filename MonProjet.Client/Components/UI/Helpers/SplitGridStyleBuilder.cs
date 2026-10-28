namespace MonProjet.Client.Components.UI.Helpers;

public static class SplitGridStyleBuilder
{
    public static string Build(SplitRatio ratio, string gap)
    {
        return string.Format(
            System.Globalization.CultureInfo.InvariantCulture,
            "display: grid; grid-template-columns: {0}; gap: {1}; height: 100%;",
            ratio.CssValue,
            gap);
    }
}