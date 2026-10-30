using Microsoft.AspNetCore.Components;
using MonProjet.Client.Components.UI.Core;

namespace MonProjet.Client.Components.UI.Layout;

public partial class AppDialog : AppComponentBase
{
    [Parameter] public EventCallback<bool> IsVisibleChanged { get; set; }

    [Parameter] public string Title { get; set; } = string.Empty;
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public RenderFragment? Footer { get; set; }

    [Parameter] public bool CloseOnOverlay { get; set; } = true;
    [Parameter] public string MaxWidth { get; set; } = "600px";

    private async Task CloseAsync()
    {
        IsVisible = false;
        await IsVisibleChanged.InvokeAsync(false);
    }

    private async Task OnOverlayClickAsync()
    {
        if (CloseOnOverlay)
        {
            await CloseAsync();
        }
    }
}