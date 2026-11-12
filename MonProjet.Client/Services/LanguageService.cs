using System.Globalization;

namespace MonProjet.Client.Services;

public sealed class LanguageService(ILanguagePreferenceStore preferenceStore) : ILanguageService
{
    private const string DefaultCulture = "fr-FR";

    public string CurrentCulture { get; private set; } = DefaultCulture;
    public event Action? OnLanguageChanged;

    public async Task InitializeAsync()
    {
        var savedCulture = await preferenceStore.GetSavedCultureAsync();
        ApplyCulture(GetSavedCultureOrDefault(savedCulture));
    }

    public async Task ChangeLanguageAsync(string cultureCode)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(cultureCode);

        var culture = CultureInfo.GetCultureInfo(cultureCode);
        if (string.Equals(CurrentCulture, culture.Name, StringComparison.OrdinalIgnoreCase))
        {
            return;
        }

        await preferenceStore.SaveCultureAsync(culture.Name);
        ApplyCulture(culture);
    }

    private void ApplyCulture(CultureInfo culture)
    {
        CurrentCulture = culture.Name;
        CultureInfo.CurrentCulture = culture;
        CultureInfo.CurrentUICulture = culture;

        if (OperatingSystem.IsBrowser())
        {
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
        }

        OnLanguageChanged?.Invoke();
    }

    private static CultureInfo GetSavedCultureOrDefault(string? cultureCode)
    {
        if (string.IsNullOrWhiteSpace(cultureCode))
        {
            return CultureInfo.GetCultureInfo(DefaultCulture);
        }

        try
        {
            return CultureInfo.GetCultureInfo(cultureCode);
        }
        catch (CultureNotFoundException)
        {
            return CultureInfo.GetCultureInfo(DefaultCulture);
        }
    }
}