using Microsoft.AspNetCore.Components;
using MonProjet.Client.Components.UI.Core;
using MonProjet.Client.Services;

namespace MonProjet.Client.Components.UI.Layout;

public partial class AppDialog : AppComponentBase
{
    [Inject] private IFocusTrapService FocusTrapService { get; set; } = default!;

    [Parameter] public bool Visible { get; set; }
    [Parameter] public EventCallback<bool> VisibleChanged { get; set; }
    [Parameter] public string Title { get; set; } = string.Empty;
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public RenderFragment? Footer { get; set; }
    [Parameter] public bool CloseOnOverlay { get; set; } = true;

    private ElementReference _dialogRef;
    private bool _focusTrapActive;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (Visible && !_focusTrapActive)
        {
            await FocusTrapService.TrapAsync(_dialogRef);
            _focusTrapActive = true;
        }
        else if (!Visible && _focusTrapActive)
        {
            await FocusTrapService.ReleaseAsync(_dialogRef);
            _focusTrapActive = false;
        }
    }

    private async Task OnOverlayClick()
    {
        if (CloseOnOverlay) await Close();
    }

    private async Task Close() => await VisibleChanged.InvokeAsync(false);
}