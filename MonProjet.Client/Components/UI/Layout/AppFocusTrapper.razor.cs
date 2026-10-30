using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MonProjet.Client.Components.UI.Core;

namespace MonProjet.Client.Components.UI.Layout;

public partial class AppFocusTrapper : AppComponentBase, IAsyncDisposable
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public bool Active { get; set; } = true;

    private ElementReference _container;
    private IJSObjectReference? _module;
    private bool _isCurrentlyTrapped;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            try
            {
                _module = await JSRuntime.InvokeAsync<IJSObjectReference>(
                    "import", "./js/focusTrap.js");
            }
            catch (InvalidOperationException)
            {
            }
        }

        if (_module is null) return;

        if (Active && !_isCurrentlyTrapped)
        {
            try
            {
                await _module.InvokeVoidAsync("trap", _container);
                _isCurrentlyTrapped = true;
            }
            catch (JSDisconnectedException) {}
        }
        else if (!Active && _isCurrentlyTrapped)
        {
            await ReleaseTrapAsync();
        }
    }

    private async Task ReleaseTrapAsync()
    {
        if (_module is not null && _isCurrentlyTrapped)
        {
            try
            {
                await _module.InvokeVoidAsync("release", _container);
            }
            catch (JSDisconnectedException) {}
            finally
            {
                _isCurrentlyTrapped = false;
            }
        }
    }

    public async ValueTask DisposeAsync()
    {
        await ReleaseTrapAsync();

        if (_module is not null)
        {
            try
            {
                await _module.DisposeAsync();
            }
            catch (JSDisconnectedException) {}
            catch (TaskCanceledException) {}
        }
        GC.SuppressFinalize(this);
    }
}