namespace MonProjet.Client.Services;

public interface ILanguageService
{
    string CurrentCulture { get; }
    Task InitializeAsync();
    Task ChangeLanguageAsync(string cultureCode);
}
