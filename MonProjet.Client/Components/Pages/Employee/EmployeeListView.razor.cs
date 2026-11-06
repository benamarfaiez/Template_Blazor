using Microsoft.AspNetCore.Components;
using MonProjet.Client.Components.UI.Collections.Column;
using MonProjet.Client.Models.Pages.Employee;
using MonProjet.Client.Models.UI.Menu;
using MonProjet.Client.Presenters.Employee;
using MonProjet.Client.View.Employee;

namespace MonProjet.Client.Components.Pages.Employee;

public partial class EmployeeListView : OwningComponentBase, IEmployeeView, IDisposable
{
    private IEmployeePresenter Presenter => ScopedServices.GetRequiredService<IEmployeePresenter>();

    private List<GridColumn<EmployeeDto>> _columns = null!;
    private string _selectedEmployeeName = string.Empty;

    protected override void OnInitialized()
    {
        _columns = EmployeeGridConfig.CreateColumns();
    }

    protected override async Task OnInitializedAsync()
    {
        Presenter.SetView(this);
        await Presenter.InitializeAsync();
        Presenter.ViewModel.ErrorMessage = "Impossible de contacter le serveur. Vérifiez votre connexion."; ;
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

    private static string GetRowClass(EmployeeDto emp)
    {
        return emp.IsActive ? "" : "row-inactive";
    }

    private void HandleRowClick(EmployeeDto emp)
    {
        _selectedEmployeeName = emp.Name;
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    private EmployeeDto? _selectedEmployee;

    private readonly List<BreadcrumbItem> _breadcrumbItems =
    [
        new("Accueil", "/", "🏠"),
        new("Ressources Humaines", "/"),
        new("Employés") // Le dernier n'a pas de Href car c'est la page actuelle
    ];
}