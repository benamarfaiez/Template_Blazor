using MonProjet.Client.Models.Employee;
using MonProjet.Client.Services;
using MonProjet.Client.View.Employee;

namespace MonProjet.Client.Presenters.Employee;

public class EmployeePresenter(IExceptionHandler exceptions) : IEmployeePresenter
{
    private IEmployeeView? _view;
    private readonly IExceptionHandler _exceptions = exceptions;

    public EmployeeViewModel ViewModel { get; } = new();

    public void SetView(IEmployeeView view) => _view = view;

    public async Task InitializeAsync()
    {
        await LoadEmployeesAsync();
    }

    public async Task LoadEmployeesAsync()
    {
        ViewModel.IsLoading = true;
        ViewModel.ErrorMessage = null;
        _view?.ShowLoading(true);

        try
        {
            // Simulation d'un appel API
            await Task.Delay(600);

            ViewModel.Employees =
            [
                new() { Id = 1, Name = "Alice Dupont", Email = "alice@test.com", Role = "Admin", IsActive = true, HireDate = new DateTime(2020, 5, 12) },
                new() { Id = 2, Name = "Bob Martin", Email = "bob@test.com", Role = "Éditeur", IsActive = false, HireDate = new DateTime(2022, 11, 1) },
                new() { Id = 3, Name = "Charlie Durand", Email = "charlie@test.com", Role = "Lecteur", IsActive = true, HireDate = new DateTime(2023, 2, 15) }
            ];
        }
        catch (Exception ex)
        {
            await _exceptions.HandleAsync(ex, "Erreur lors du chargement des employés.");
            ViewModel.ErrorMessage = "Impossible de charger la liste des employés.";
            _view?.ShowError(ViewModel.ErrorMessage);
        }
        finally
        {
            ViewModel.IsLoading = false;
            _view?.ShowLoading(false);
        }
    }
}