using System.Globalization;
using System.Numerics;
using Microsoft.AspNetCore.Components;
using MonProjet.Client.Components.UI.Core;

namespace MonProjet.Client.Components.UI.BasicInput;

public partial class AppNumberBox<TValue> : AppComponentBase
    where TValue : struct, INumber<TValue>
{
    [Parameter] public TValue Value { get; set; }
    [Parameter] public EventCallback<TValue> ValueChanged { get; set; }

    [Parameter] public TValue? Min { get; set; }
    [Parameter] public TValue? Max { get; set; }
    [Parameter] public TValue Step { get; set; } = TValue.One;

    [Parameter] public string Placeholder { get; set; } = string.Empty;
    [Parameter] public bool Disabled { get; set; }

    private bool CanDecrement => !Disabled && (Min is null || Value > Min.Value);
    private bool CanIncrement => !Disabled && (Max is null || Value < Max.Value);
    private string GetFormattedValue()
        => Value.ToString("G", CultureInfo.InvariantCulture);

    private async Task HandleInput(ChangeEventArgs e)
    {
        var stringValue = e.Value?.ToString();

        if (string.IsNullOrWhiteSpace(stringValue))
        {
            await UpdateValueAsync(Min ?? TValue.Zero);
            return;
        }

        stringValue = stringValue.Replace(',', '.');

        if (TValue.TryParse(stringValue, NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
        {
            await UpdateValueAsync(result);
        }
        else
        {
            StateHasChanged();
        }
    }

    private async Task Increment()
    {
        if (!CanIncrement) return;

        var newValue = Value + Step;
        if (Max is not null && newValue > Max.Value) newValue = Max.Value;

        await UpdateValueAsync(newValue);
    }

    private async Task Decrement()
    {
        if (!CanDecrement) return;

        var newValue = Value - Step;
        if (Min is not null && newValue < Min.Value) newValue = Min.Value;

        await UpdateValueAsync(newValue);
    }

    private async Task UpdateValueAsync(TValue newValue)
    {
        if (EqualityComparer<TValue>.Default.Equals(Value, newValue)) return;

        if (Min is not null && newValue < Min.Value) newValue = Min.Value;
        if (Max is not null && newValue > Max.Value) newValue = Max.Value;

        Value = newValue;
        await ValueChanged.InvokeAsync(Value);
    }
}