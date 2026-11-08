namespace MonProjet.Client.Services;

public interface IThemeService
{
    bool IsDarkMode { get; }
    event Action? OnThemeChanged;

    Task InitializeAsync();
    Task ToggleThemeAsync();
}