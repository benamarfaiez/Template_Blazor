using Microsoft.AspNetCore.Components;
using MonProjet.Client.Components.UI.Core;
using MonProjet.Client.Components.UI.Enums;

namespace MonProjet.Client.Components.UI.Feedback;

public partial class AppProgressBar : AppComponentBase
{
    [Parameter] public double Value { get; set; }
    [Parameter] public bool Indeterminate { get; set; }
    [Parameter] public ProgressVariant Variant { get; set; } = ProgressVariant.Primary;
    [Parameter] public string BarHeight { get; set; } = "8px";

    private double ClampedValue => Math.Clamp(Value, 0, 100);
    private string AriaValue => ClampedValue.ToString("F0", System.Globalization.CultureInfo.InvariantCulture);
    private string VariantClass => $"app-progressbar-{Variant.ToString().ToLowerInvariant()}";

}