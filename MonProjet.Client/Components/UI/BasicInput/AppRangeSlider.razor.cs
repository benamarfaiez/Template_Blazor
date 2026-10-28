using Microsoft.AspNetCore.Components;
using MonProjet.Client.Components.UI.Core;
using System.Numerics;

namespace MonProjet.Client.Components.UI.BasicInput;

public partial class AppRangeSlider<TValue> : AppComponentBase
    where TValue : struct, INumber<TValue>
{
    [Parameter] public TValue Value { get; set; }
    [Parameter] public EventCallback<TValue> ValueChanged { get; set; }
    [Parameter] public TValue Min { get; set; } = TValue.Zero;
    [Parameter] public TValue Max { get; set; } = TValue.CreateChecked(100);
    [Parameter] public TValue Step { get; set; } = TValue.One;
    [Parameter] public bool Disabled { get; set; }

    private string FillPercentage
    {
        get
        {
            if (Max == Min) return "0%";

            double dValue = double.CreateChecked(Value);
            double dMin = double.CreateChecked(Min);
            double dMax = double.CreateChecked(Max);

            double percentage = ((dValue - dMin) / (dMax - dMin)) * 100;

            percentage = Math.Clamp(percentage, 0, 100);

            return $"{percentage.ToString(System.Globalization.CultureInfo.InvariantCulture)}%";
        }
    }

    private async Task OnInputAsync(ChangeEventArgs e)
    {
        if (Disabled) return;

        var stringValue = e.Value?.ToString();
        if (string.IsNullOrWhiteSpace(stringValue)) return;

        if (TValue.TryParse(stringValue, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.CurrentCulture, out var newValue))
        {
            if (newValue < Min) newValue = Min;
            if (newValue > Max) newValue = Max;

            if (EqualityComparer<TValue>.Default.Equals(Value, newValue)) return;

            Value = newValue;
            await ValueChanged.InvokeAsync(Value);
        }
    }
}