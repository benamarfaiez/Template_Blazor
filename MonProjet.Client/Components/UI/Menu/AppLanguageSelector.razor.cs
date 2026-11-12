using Microsoft.AspNetCore.Components;
using MonProjet.Client.Services;

namespace MonProjet.Client.Components.UI.Menu;

public partial class AppLanguageSelector : ComponentBase, IDisposable
{
    [Inject]
    public ILanguageService LanguageService { get; set; } = default!;
    [Inject]
    private NavigationManager Navigation { get; set; } = default!;
    private bool _isMenuOpen;
    private const string CultureFR = "fr-FR";
    private const string CultureEN = "en-US";

    private string CurrentLanguageLabel => LanguageService.CurrentCulture.StartsWith("fr") ? "🇫🇷 FR" : "🇬🇧 EN";

    protected override void OnInitialized()
    {
        LanguageService.OnLanguageChanged += HandleLanguageChanged;
    }

    private async Task ChangeLanguageAsync(string culture)
    {
        _isMenuOpen = false;
        var previousCulture = LanguageService.CurrentCulture;
        await LanguageService.ChangeLanguageAsync(culture);

        if (!OperatingSystem.IsBrowser() && !string.Equals(previousCulture, LanguageService.CurrentCulture, StringComparison.OrdinalIgnoreCase))
        {
            Navigation.NavigateTo(Navigation.Uri, forceLoad: true);
        }
    }

    private void HandleLanguageChanged()
    {
        InvokeAsync(StateHasChanged);
    }

    public void Dispose()
    {
        LanguageService.OnLanguageChanged -= HandleLanguageChanged;
        GC.SuppressFinalize(this);
    }
}
