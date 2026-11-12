using Microsoft.JSInterop;
using System.Globalization;

namespace MonProjet.Client.Services;

public class LanguageService(IJSRuntime js) : ILanguageService
{
    private readonly IJSRuntime _js = js;
    private IJSObjectReference? _module;

    public string CurrentCulture { get; private set; } = "fr-FR";
    public event Action? OnLanguageChanged;

    private async Task<IJSObjectReference> GetModuleAsync()
    {
        _module ??= await _js.InvokeAsync<IJSObjectReference>("import", "./js/language.js");
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
                OnLanguageChanged?.Invoke();
                return;
            }

            ApplyCulture("fr-FR");
            OnLanguageChanged?.Invoke();
        }
        catch (Exception)
        {
        }
    }

    public async Task ChangeLanguageAsync(string cultureCode)
    {
        if (CurrentCulture == cultureCode) return;
        ApplyCulture(cultureCode);
        OnLanguageChanged?.Invoke();
    }

    private void ApplyCulture(string cultureCode)
    {
        CurrentCulture = cultureCode;
        var cultureInfo = new CultureInfo(cultureCode);
        CultureInfo.CurrentCulture = cultureInfo;
        CultureInfo.CurrentUICulture = cultureInfo;
        if (OperatingSystem.IsBrowser())
        {
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
        }
    }
}