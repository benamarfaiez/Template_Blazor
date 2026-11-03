using MonProjet.Client.Models.Pages.Employee;
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
            ViewModel.Employees =
            [
                new() { Id = 1, Name = "Alice Dupont", Email = "alice@test.com", Role = "Admin", HireDate = new DateTime(2020, 1, 15) },
                new() { Id = 2, Name = "Bob Martin", Email = "bob@test.com", Role = "Éditeur", HireDate = new DateTime(2021, 3, 20) },
                new() { Id = 3, Name = "Charlie Durand", Email = "charlie@test.com", Role = "Lecteur", HireDate = new DateTime(2022, 7, 10) },
                new() { Id = 4, Name = "David Lemaire", Email = "david@test.com", Role = "Admin", HireDate = new DateTime(2019, 11, 5) },
                new() { Id = 5, Name = "Eva Rousseau", Email = "eva@test.com", Role = "Éditeur", HireDate = new DateTime(2023, 2, 28) },
                new() { Id = 6, Name = "Frank Moreau", Email = "frank@test.com", Role = "Lecteur", HireDate = new DateTime(2020, 9, 12) },
                new() { Id = 7, Name = "Grace Lefevre", Email = "grace@test.com", Role = "Admin", HireDate = new DateTime(2021, 6, 15) },
                new() { Id = 8, Name = "Hugo Petit", Email = "hugo@test.com", Role = "Éditeur", HireDate = new DateTime(2022, 1, 10) },
                new() { Id = 9, Name = "Isabelle Garnier", Email = "isabelle@test.com", Role = "Lecteur", HireDate = new DateTime(2023, 4, 20) },
                new() { Id = 10, Name = "Julien Mercier", Email = "julien@test.com", Role = "Admin", HireDate = new DateTime(2019, 8, 5) },
                new() { Id = 11, Name = "Karine Lefevre", Email = "karine@test.com", Role = "Éditeur", HireDate = new DateTime(2020, 12, 15) },
                new() { Id = 12, Name = "Louis Morel", Email = "louis@test.com", Role = "Lecteur", HireDate = new DateTime(2021, 5, 10) },
                new() { Id = 13, Name = "Marie Dubois", Email = "marie@test.com", Role = "Admin", HireDate = new DateTime(2022, 9, 20) },
                new() { Id = 14, Name = "Nicolas Garnier", Email = "nicolas@test.com", Role = "Éditeur", HireDate = new DateTime(2023, 1, 5) },
                new() { Id = 15, Name = "Olivia Lefevre", Email = "olivia@test.com", Role = "Lecteur", HireDate = new DateTime(2019, 7, 15) },
                new() { Id = 16, Name = "Pauline Moreau", Email = "pauline@test.com", Role = "Admin", HireDate = new DateTime(2020, 4, 10) },

                new() { Id = 17, Name = "Quentin Petit", Email = "quentin@test.com", Role = "Éditeur", HireDate = new DateTime(2021, 8, 20) },
                new() { Id = 18, Name = "Romain Lefevre", Email = "romain@test.com", Role = "Lecteur", HireDate = new DateTime(2022, 3, 15) },
                new() { Id = 19, Name = "Sophie Garnier", Email = "sophie@test.com", Role = "Admin", HireDate = new DateTime(2023, 6, 5) }
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