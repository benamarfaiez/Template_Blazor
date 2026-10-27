using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace MonProjet.Client.Services;

public class FocusTrapService(IJSRuntime js) : IFocusTrapService
{
    private readonly IJSRuntime _js = js;
    private IJSObjectReference? _module;

    public async Task TrapAsync(ElementReference element)
    {
        _module ??= await _js.InvokeAsync<IJSObjectReference>(
            "import", "./js/focusTrap.js");
        await _module.InvokeVoidAsync("trap", element);
    }

    public async Task ReleaseAsync(ElementReference element)
    {
        if (_module is not null)
        {
            await _module.InvokeVoidAsync("release", element);
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (_module is not null)
        {
            try { await _module.DisposeAsync(); }
            catch (JSDisconnectedException) { /* Ignorer si le circuit est fermé */ }
            catch (TaskCanceledException) { }
        }
    }
}