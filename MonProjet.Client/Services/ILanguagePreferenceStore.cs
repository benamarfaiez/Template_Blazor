namespace MonProjet.Client.Services;

public interface ILanguagePreferenceStore
{
    Task<string?> GetSavedCultureAsync();
    Task SaveCultureAsync(string cultureCode);
    Task SetDocumentLanguageAsync(string cultureCode);
}
