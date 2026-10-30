using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MonProjet.Client.Components.UI.Core;
using MonProjet.Client.Components.UI.Enums;

namespace MonProjet.Client.Components.UI.Layout;

public partial class AppPopover : AppComponentBase
{
    [Parameter] public RenderFragment? Anchor { get; set; }
    [Parameter] public RenderFragment? Content { get; set; }
    [Parameter] public bool IsOpen { get; set; }
    [Parameter] public EventCallback<bool> IsOpenChanged { get; set; }

    [Parameter] public PopoverPosition Position { get; set; } = PopoverPosition.Bottom;
    [Parameter] public bool TriggerOnHover { get; set; } = false;

    private async Task ToggleAsync()
    {
        if (TriggerOnHover) return;
        await SetOpenAsync(!IsOpen);
    }

    private async Task OnMouseEnterAsync()
    {
        if (!TriggerOnHover) return;
        await SetOpenAsync(true);
    }

    private async Task OnMouseLeaveAsync()
    {
        if (!TriggerOnHover) return;
        await SetOpenAsync(false);
    }

    private async Task HandleFocusOutAsync(FocusEventArgs e)
    {
        if (TriggerOnHover) return;
        if (IsOpen) await SetOpenAsync(false);
    }

    private async Task SetOpenAsync(bool value)
    {
        if (IsOpen == value) return;

        IsOpen = value;
        await IsOpenChanged.InvokeAsync(IsOpen);
    }
}