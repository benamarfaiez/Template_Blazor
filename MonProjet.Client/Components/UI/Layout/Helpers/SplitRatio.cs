namespace MonProjet.Client.Components.UI.Layout.Helpers;

public sealed class SplitRatio
{
    public string CssValue { get; }

    private SplitRatio(string cssValue) => CssValue = cssValue;

    // Ratios prédéfinis
    public static readonly SplitRatio OneToOne = new("1fr 1fr");
    public static readonly SplitRatio OneToTwo = new("1fr 2fr");
    public static readonly SplitRatio TwoToOne = new("2fr 1fr");
    public static readonly SplitRatio LeftFixed = new("300px 1fr");
    public static readonly SplitRatio RightFixed = new("1fr 300px");
    public static SplitRatio Custom(string value) => new(value);
}