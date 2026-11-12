using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MonProjet.Client.Components.UI.Collections.Column;
using MonProjet.Client.Presenters.Home;
using MonProjet.Client.Resources;
using MonProjet.Client.Services;
using MonProjet.Client.View.Home;
using MonProjet.Shared.Models.Home;

namespace MonProjet.Client.Components.Pages.Home;

public partial class HomeView : OwningComponentBase, IHomeView
{
    [Inject] public IStringLocalizer<HomeResource> Loc { get; set; } = default!;
    [Inject] public ILanguageService LanguageService { get; set; } = default!;

    private IHomePresenter Presenter { get; set; } = default!;

    private string _searchTerm = string.Empty;
    public string SearchTerm
    {
        get => _searchTerm;
        set => _searchTerm = value;
    }

    private List<GridColumn<HomeItemDto>> _columns = null!;

    protected override void OnInitialized()
    {
        Presenter = ScopedServices.GetRequiredService<IHomePresenter>();
        _columns = HomeGridConfig.CreateColumns(Loc);
    }

    protected override async Task OnInitializedAsync()
    {
        Presenter.SetView(this);
        await Presenter.InitializeAsync();
        Presenter.ViewModel.ErrorMessage = Loc["Error_ServiceUnavailable"];
    }

    private Task RefreshDataAsync() => Presenter.RefreshDataAsync();

    public void ShowError(string message)
    {
        Presenter.ViewModel.ErrorMessage = message;
        StateHasChanged();
    }

    public void ShowLoading(bool isLoading)
    {
        Presenter.ViewModel.IsLoading = isLoading;
        StateHasChanged();
    }
}