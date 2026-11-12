using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Localization;
using MonProjet.Client.Models.UI.Menu.NavBar;
using MonProjet.Client.Resources;
using MonProjet.Client.Services;
using MonProjet.Client.Services.Navigation;
using System.Globalization;

namespace MonProjet.Client.Components.Layout;

public partial class MainLayout : LayoutComponentBase, IDisposable
{
    [Inject] public IStringLocalizer<SharedResource> Loc { get; set; } = default!;
    [Inject] public ILanguageService LanguageService { get; set; } = default!;
    [Inject] public IMainNavigationProvider NavigationProvider { get; set; } = default!;
    private List<NavLinkItem> _navLinks = [];
    private ErrorBoundary? _errorBoundary;

    private void Recover() => _errorBoundary?.Recover();

    protected override void OnInitialized()
    {
        LanguageService.OnLanguageChanged += HandleLanguageChanged;
        BuildNavLinks();
    }
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await LanguageService.InitializeAsync();
        }
    }
    private void HandleLanguageChanged()
    {
        _ = InvokeAsync(() =>
        {
            var culture = CultureInfo.GetCultureInfo(LanguageService.CurrentCulture);
            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;
            BuildNavLinks();
            StateHasChanged();
        });
    }
    private void BuildNavLinks() => _navLinks = NavigationProvider.CreateLinks();
    public void Dispose()
    {
        LanguageService.OnLanguageChanged -= HandleLanguageChanged;
        GC.SuppressFinalize(this);
    }
}