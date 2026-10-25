using MonProjet.Client.Models.User;
using MonProjet.Client.Services;
using MonProjet.Client.View.User;

namespace MonProjet.Client.Presenters.Users;

public class UserPresenter(IExceptionHandler exceptions) : IUserPresenter
{
    private IUserView? _view;
    private readonly IExceptionHandler _exceptions = exceptions;

    public UserViewModel ViewModel { get; } = new();

    public void SetView(IUserView view) => _view = view;

    public async Task InitializeAsync()
    {
        await LoadUsersAsync();
    }

    public async Task LoadUsersAsync()
    {
        ViewModel.IsLoading = true;
        ViewModel.ErrorMessage = null;
        _view?.ShowLoading(true);

        try
        {
            // Simulation d'un appel API
            await Task.Delay(400);

            ViewModel.Users =
            [
                new(1, "Alice Dupont", "alice@example.com", "Admin"),
                new(2, "Bob Martin", "bob@example.com", "Éditeur"),
                new(3, "Charlie Durand", "charlie@example.com", "Lecteur")
            ];
        }
        catch (Exception ex)
        {
            await _exceptions.HandleAsync(ex, "Erreur de chargement des utilisateurs.");
            ViewModel.ErrorMessage = "Impossible de charger les utilisateurs.";
            _view?.ShowError(ViewModel.ErrorMessage);
        }
        finally
        {
            ViewModel.IsLoading = false;
            _view?.ShowLoading(false);
        }
    }
}