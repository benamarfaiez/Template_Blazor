using Microsoft.JSInterop;

namespace MonProjet.Client.Services;

public class ThemeService(IJSRuntime js) : IThemeService
{
    private readonly IJSRuntime _js = js;
    private IJSObjectReference? _module;

    public bool IsDarkMode { get; private set; }
    public event Action? OnThemeChanged;

    public async Task InitializeAsync()
    {
        try
        {
            _module ??= await _js.InvokeAsync<IJSObjectReference>("import", "./js/theme.js");
            IsDarkMode = await _module.InvokeAsync<bool>("getTheme");
            OnThemeChanged?.Invoke();
        }
        catch (Exception)
        {
        }
    }

    public async Task ToggleThemeAsync()
    {
        IsDarkMode = !IsDarkMode;

        if (_module is not null)
        {
            await _module.InvokeVoidAsync("setTheme", IsDarkMode);
        }
        else
        {
            await _js.InvokeVoidAsync("eval", $"document.documentElement.classList.toggle('dark-mode', {IsDarkMode.ToString().ToLower()})");
        }

        OnThemeChanged?.Invoke();
    }
}