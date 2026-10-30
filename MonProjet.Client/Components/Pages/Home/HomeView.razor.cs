using Microsoft.AspNetCore.Components;
using MonProjet.Client.Components.UI.Collections.Column;
using MonProjet.Client.Models.Home;
using MonProjet.Client.Presenters.Home;
using MonProjet.Client.View.Home;

namespace MonProjet.Client.Components.Pages.Home;

public partial class HomeView : OwningComponentBase, IHomeView, IDisposable
{
    private IHomePresenter Presenter => ScopedServices.GetRequiredService<IHomePresenter>();

    // Propriétés liées à la recherche
    private string _searchTerm = string.Empty;
    public string SearchTerm
    {
        get => _searchTerm;
        set
        {
            if (_searchTerm != value)
            {
                _searchTerm = value;
                RequestRefresh();
            }
        }
    }

    public event Action? OnRefreshRequested;

    private List<GridColumn<HomeItem>> _columns = null!;

    protected override void OnInitialized()
    {
        _columns = HomeGridConfig.CreateColumns();
    }

    protected override async Task OnInitializedAsync()
    {
        Presenter.SetView(this);
        await Presenter.InitializeAsync();
        Presenter.ViewModel.ErrorMessage = "Impossible de contacter le serveur. Vérifiez votre connexion.";
    }

    private void RequestRefresh()
    {
        OnRefreshRequested?.Invoke();
    }

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

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}