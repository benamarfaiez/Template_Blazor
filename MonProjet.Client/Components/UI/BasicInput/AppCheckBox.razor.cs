using Microsoft.AspNetCore.Components;
using MonProjet.Client.Components.UI.Core;

namespace MonProjet.Client.Components.UI.BasicInput;

public partial class AppCheckBox : AppComponentBase
{
    [Parameter] public bool Value { get; set; }
    [Parameter] public EventCallback<bool> ValueChanged { get; set; }
    [Parameter] public string Label { get; set; } = string.Empty;
    [Parameter] public bool Disabled { get; set; }
    private Task OnChanged(ChangeEventArgs e)
    {
        if (Disabled) return Task.CompletedTask;

        var newValue = e.Value is true;
        Value = newValue;
        return ValueChanged.InvokeAsync(newValue);
    }
}