using Microsoft.JSInterop;
using System.Globalization;

namespace MonProjet.Client.Services;

public class LanguageService(IJSRuntime js) : ILanguageService
{
    private readonly IJSRuntime _js = js;
    private IJSObjectReference? _module;

    public string CurrentCulture { get; private set; } = "fr-FR";

    private async Task<IJSObjectReference> GetModuleAsync()
    {
        if (_module is not null)
        {
            return _module;
        }
        _module = await _js.InvokeAsync<IJSObjectReference>("import", "./js/language.js");
        return _module;
    }

    public async Task InitializeAsync()
    {
        try
        {
            var module = await GetModuleAsync();
            var savedCulture = await module.InvokeAsync<string>("getLanguage");

            if (!string.IsNullOrEmpty(savedCulture))
            {
                ApplyCulture(savedCulture);
            }
        }
        catch (Exception)
        {
            // On ignore l'erreur de prérendu serveur
        }
    }

    public async Task ChangeLanguageAsync(string cultureCode)
    {
        if (CurrentCulture == cultureCode) return;

        ApplyCulture(cultureCode);

        try
        {
            // ✅ On récupère le module (il sera chargé s'il était null)
            var module = await GetModuleAsync();
            await module.InvokeVoidAsync("setLanguage", cultureCode);
        }
        catch (JSDisconnectedException)
        {
            // Si le circuit est fermé, on ignore
        }
        catch (InvalidOperationException)
        {
            // Si on est en prérendu (ne devrait pas arriver sur un clic)
        }
    }

    private void ApplyCulture(string cultureCode)
    {
        CurrentCulture = cultureCode;
        var cultureInfo = new CultureInfo(cultureCode);
        CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
        CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
    }
}