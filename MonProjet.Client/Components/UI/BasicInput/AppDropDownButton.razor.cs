using Microsoft.AspNetCore.Components;
using MonProjet.Client.Components.UI.Core;
using MonProjet.Client.Components.UI.Enums;

namespace MonProjet.Client.Components.UI.BasicInput;

public partial class AppDropDownButton : AppComponentBase
{
    [Parameter] public string Text { get; set; } = string.Empty;
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public bool IsOpen { get; set; }
    [Parameter] public EventCallback<bool> IsOpenChanged { get; set; }
    [Parameter] public ButtonVariant Variant { get; set; } = ButtonVariant.Primary;
    private async Task ToggleAsync()
    {
        IsOpen = !IsOpen;
        await IsOpenChanged.InvokeAsync(IsOpen);
    }

    private async Task CloseAsync()
    {
        IsOpen = false;
        await IsOpenChanged.InvokeAsync(IsOpen);
    }
}