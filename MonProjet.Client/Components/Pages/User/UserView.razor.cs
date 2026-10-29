using Microsoft.AspNetCore.Components;
using MonProjet.Client.Components.UI.Collections.Column;
using MonProjet.Client.Models.User;
using MonProjet.Client.Presenters.Users;
using MonProjet.Client.View.User;

namespace MonProjet.Client.Components.Pages.User;

public partial class UserView : OwningComponentBase, IUserView, IDisposable
{
    // 1. Résolution du présentateur via le scope du composant
    private IUserPresenter Presenter => ScopedServices.GetRequiredService<IUserPresenter>();

    // 2. Configuration de la grille (déléguée)
    private List<GridColumn<UserDto>> _columns = null!;

    private List<UserDto> _users = [];
    private IEnumerable<UserDto> _selectedUsers = [];
    protected override void OnInitialized()
    {
        // Initialisation des colonnes
        _columns = UserGridConfig.CreateColumns();
        _users = UserGridConfig.GetSampleUsers();
    }

    protected override async Task OnInitializedAsync()
    {
        Presenter.SetView(this);
        await Presenter.InitializeAsync();
    }

    // 3. Implémentation de IView
    public void ShowError(string message) => StateHasChanged();
    public void ShowLoading(bool isLoading) => StateHasChanged();

    // 4. Implémentation de IDisposable
    public void Dispose()
    {
        // Le scope du OwningComponentBase libère automatiquement le présentateur.
        GC.SuppressFinalize(this);
    }
}