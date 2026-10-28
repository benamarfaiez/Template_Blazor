using Microsoft.AspNetCore.Components;
using MonProjet.Client.Components.UI.Core;

namespace MonProjet.Client.Components.UI.BasicInput;

public partial class AppRadioButton : AppComponentBase
{
    [Parameter] public string GroupName { get; set; } = "radio-group";
    [Parameter] public string Value { get; set; } = string.Empty;
    [Parameter] public string? CurrentValue { get; set; }
    [Parameter] public EventCallback<string> CurrentValueChanged { get; set; }
    [Parameter] public string Label { get; set; } = string.Empty;
    [Parameter] public bool Disabled { get; set; }

    private Task OnChanged()
    {
        if (Disabled) return Task.CompletedTask;

        return CurrentValueChanged.InvokeAsync(Value);
    }
}