namespace MonProjet.Client.Components.UI.Feedback;

public partial class ThemeToggle
{
    private bool _hasInitialized = false;
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender && !_hasInitialized)
        {
            await ThemeService.InitializeAsync();
            ThemeService.OnThemeChanged += StateHasChanged;
            _hasInitialized = true;
            StateHasChanged();
        }
    }

    private async Task ToggleTheme()
    {
        await ThemeService.ToggleThemeAsync();
    }

    public void Dispose()
    {
        ThemeService.OnThemeChanged -= StateHasChanged;
        GC.SuppressFinalize(this);
    }
}
