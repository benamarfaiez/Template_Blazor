namespace MonProjet.Client.Services;

public interface ILanguageService
{
    string CurrentCulture { get; }
    event Action? OnLanguageChanged;
    Task InitializeAsync();
    Task ChangeLanguageAsync(string cultureCode);
}
