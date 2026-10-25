using MonProjet.Client.Models.Home;
using MonProjet.Client.Services;
using MonProjet.Client.View.Home;

namespace MonProjet.Client.Presenters.Home;

public class HomePresenter(IExceptionHandler exceptions) : IHomePresenter
{
    private IHomeView? _view;
    private readonly IExceptionHandler _exceptions = exceptions;
    private Action? _refreshHandler;

    public HomeViewModel ViewModel { get; } = new();

    public void SetView(IHomeView view)
    {
        if (_refreshHandler is not null && _view is not null)
            _view.OnRefreshRequested -= _refreshHandler;

        _view = view;
        _refreshHandler = async () => await RefreshDataAsync();
        view.OnRefreshRequested += _refreshHandler;
    }

    public async Task InitializeAsync()
    {
        ViewModel.IsLoading = true;
        ViewModel.ErrorMessage = null;
        _view?.ShowLoading(true);
        try
        {
            await Task.Delay(200);
            ViewModel.Items = BuildSampleData();
        }
        catch (Exception ex)
        {
            await _exceptions.HandleAsync(ex, "Impossible de charger les donnÃ©es.");
            ViewModel.ErrorMessage = "Erreur de chargement des donnÃ©es.";
            _view?.ShowError(ViewModel.ErrorMessage);
        }
        finally
        {
            ViewModel.IsLoading = false;
            _view?.ShowLoading(false);
        }
    }

    public async Task RefreshDataAsync()
    {
        ViewModel.IsLoading = true;
        _view?.ShowLoading(true);

        try
        {
            // Si l'utilisateur tape une autre lettre, cette tâche est annulée/ignorée
            // et une nouvelle commence.
            await Task.Delay(300);

            var filter = _view?.SearchTerm ?? string.Empty;

            // On simule un appel API
            var data = BuildSampleData();

            ViewModel.Items = string.IsNullOrWhiteSpace(filter)
                ? data
                : [.. data.Where(i => i.Name.Contains(filter, StringComparison.OrdinalIgnoreCase))];
        }
        catch (Exception ex)
        {
            await _exceptions.HandleAsync(ex, "Erreur lors de la recherche.");
            ViewModel.ErrorMessage = "Erreur de recherche.";
        }
        finally
        {
            ViewModel.IsLoading = false;
            _view?.ShowLoading(false);
        }
    }

    private static List<HomeItem> BuildSampleData() =>
    [
        new(1, "Projet Alpha", "Actif", DateTime.UtcNow.AddDays(-3)),
        new(2, "Projet Beta", "En attente", DateTime.UtcNow.AddDays(-1)),
        new(3, "Projet Gamma", "TerminÃ©", DateTime.UtcNow.AddDays(-10)),
    ];
}