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
            await EnsureModuleAsync();
            IsDarkMode = await _module!.InvokeAsync<bool>("getTheme");
            OnThemeChanged?.Invoke();
        }
        catch (Exception)
        {
        }
    }

    public async Task ToggleThemeAsync()
    {
        try
        {
            await EnsureModuleAsync();
            var nextIsDarkMode = !IsDarkMode;
            await _module!.InvokeVoidAsync("setTheme", nextIsDarkMode);
            IsDarkMode = nextIsDarkMode;
            OnThemeChanged?.Invoke();
        }
        catch (Exception)
        {
        }
    }

    private async Task EnsureModuleAsync()
    {
        _module ??= await _js.InvokeAsync<IJSObjectReference>("import", "./js/theme.js");
    }
}