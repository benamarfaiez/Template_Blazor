using Microsoft.AspNetCore.Components;
using MonProjet.Client.Services;

namespace MonProjet.Client.Components.UI.Menu;

public partial class AppLanguageSelector
{
    [Inject]
    public ILanguageService LanguageService { get; set; } = default!;
    private const string CultureFR = "fr-FR";
    private const string CultureEN = "en-US";

    private string CurrentLanguageLabel => LanguageService.CurrentCulture.StartsWith("fr") ? "🇫🇷 FR" : "🇬🇧 EN";

    private async Task ChangeLanguageAsync(string culture)
    {
        await LanguageService.ChangeLanguageAsync(culture);
    }
}
