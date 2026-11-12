using Microsoft.JSInterop;

namespace MonProjet.Client.Services;

public sealed class JsLanguagePreferenceStore(IJSRuntime js) : ILanguagePreferenceStore, IAsyncDisposable
{
    private IJSObjectReference? _module;

    public async Task<string?> GetSavedCultureAsync()
    {
        var module = await GetModuleAsync();
        return await module.InvokeAsync<string?>("getLanguage");
    }

    public async Task SaveCultureAsync(string cultureCode)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(cultureCode);

        var module = await GetModuleAsync();
        await module.InvokeVoidAsync("setLanguage", cultureCode);
    }

    private async Task<IJSObjectReference> GetModuleAsync()
    {
        _module ??= await js.InvokeAsync<IJSObjectReference>("import", "./js/language.js");
        return _module;
    }

    public async ValueTask DisposeAsync()
    {
        if (_module is not null)
        {
            try
            {
                await _module.DisposeAsync();
            }
            catch (JSDisconnectedException)
            {
            }
        }
    }
}
