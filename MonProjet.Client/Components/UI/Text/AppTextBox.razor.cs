using Microsoft.AspNetCore.Components;
using MonProjet.Client.Components.UI.Core;

namespace MonProjet.Client.Components.UI.Text;

public partial class AppTextBox : AppComponentBase
{
    [Parameter] public string Value { get; set; } = string.Empty;
    [Parameter] public EventCallback<string> ValueChanged { get; set; }
    [Parameter] public string Placeholder { get; set; } = string.Empty;
    [Parameter] public string InputType { get; set; } = "text";
    [Parameter] public bool Disabled { get; set; }
    [Parameter] public bool ReadOnly { get; set; }
    [Parameter] public int? MaxLength { get; set; }
    [Parameter] public string? Id { get; set; }
    [Parameter] public string? Name { get; set; }
    [Parameter] public Func<string, bool>? ValidationFunc { get; set; }
    [Parameter] public string? ErrorMessage { get; set; }
    [Parameter] public bool ForceShowError { get; set; }
    [Parameter] public string? LeadingIcon { get; set; }
    [Parameter] public string? TrailingIcon { get; set; }
    [Parameter] public bool ShowClearButton { get; set; }

    private string _calculatedId = string.Empty;

    private bool HasError
    {
        get
        {
            return ForceShowError ||
                   (ValidationFunc is not null &&
                    !string.IsNullOrEmpty(Value) &&
                    !ValidationFunc(Value));
        }
    }

    private string AriaDescribedBy => HasError && !string.IsNullOrEmpty(ErrorMessage) ? $"{_calculatedId}-error" : string.Empty;

    protected override void OnInitialized()
    {
        _calculatedId = Id ?? $"app-input-{Guid.NewGuid():N}";
    }

    private async Task OnInputAsync(ChangeEventArgs e)
    {
        if (Disabled || ReadOnly) return;

        Value = e.Value?.ToString() ?? string.Empty;
        await ValueChanged.InvokeAsync(Value);
    }

    private async Task ClearAsync()
    {
        if (Disabled || ReadOnly) return;

        Value = string.Empty;
        await ValueChanged.InvokeAsync(Value);
    }
}