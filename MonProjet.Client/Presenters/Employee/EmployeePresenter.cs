using MonProjet.Client.Models.Pages.Employee;
using MonProjet.Client.Services;
using MonProjet.Client.View.Employee;
using MonProjet.Shared.Models.Employee;

namespace MonProjet.Client.Presenters.Employee;

public class EmployeePresenter(IExceptionHandler exceptions, IToastService toastService) : IEmployeePresenter
{
    private IEmployeeView? _view;
    private readonly IExceptionHandler _exceptions = exceptions;
    private readonly IToastService _toastService = toastService;


    public EmployeeViewModel ViewModel { get; } = new();

    public void SetView(IEmployeeView view) => _view = view;

    public async Task InitializeAsync()
    {
        await LoadEmployeesAsync();
    }
    public async Task SaveEmployeeAsync()
    {
        try
        {
            // Logique de sauvegarde...
            _toastService.ShowSuccess("Employé enregistré avec succès.");
        }
        catch (Exception ex)
        {
            await _exceptions.HandleAsync(ex);
            _toastService.ShowError("Impossible d'enregistrer l'employé.");
        }
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
                new( 1, "Alice Dupont", "alice@test.com", "Admin", true, new DateTime(2020, 1, 15) ),
                new( 2, "Bob Martin", "bob@test.com", "Éditeur", false, new DateTime(2021, 3, 20) ),
                new( 3, "Charlie Durand", "charlie@test.com", "Lecteur", false, new DateTime(2022, 7, 10) ),
                new( 4, "David Lemaire", "david@test.com", "Admin", true, new DateTime(2019, 11, 5) ),
                new( 5, "Eva Rousseau", "eva@test.com", "Éditeur", false, new DateTime(2023, 2, 28) ),
                new( 6, "Frank Moreau", "frank@test.com", "Lecteur", false, new DateTime(2020, 9, 12) ),
                new( 7, "Grace Lefevre", "grace@test.com", "Admin", true, new DateTime(2021, 6, 15) ),
                new( 8, "Hugo Petit", "hugo@test.com", "Éditeur", false, new DateTime(2022, 1, 10) ),
                new( 9, "Isabelle Garnier", "isabelle@test.com", "Lecteur", false, new DateTime(2023, 4, 20) ),
                new( 10, "Julien Mercier", "julien@test.com", "Admin", true, new DateTime(2019, 8, 5) ),
                new( 11, "Karine Lefevre", "karine@test.com", "Éditeur", false, new DateTime(2020, 12, 15) ),
                new( 12, "Louis Morel", "louis@test.com", "Lecteur", false, new DateTime(2021, 5, 10) ),
                new( 13, "Marie Dubois", "marie@test.com", "Admin", true, new DateTime(2022, 9, 20) ),
                new( 14, "Nicolas Garnier", "nicolas@test.com", "Éditeur", false, new DateTime(2023, 1, 5) ),
                new( 15, "Olivia Lefevre", "olivia@test.com", "Éditeur", false, new DateTime(2023, 3, 15) ),
                new(17, "Quentin Petit", "quentin@test.com", "Éditeur", false, new DateTime(2021, 8, 20)),
                new(18, "Romain Lefevre", "romain@test.com", "Lecteur", false, new DateTime(2022, 3, 15)),
                new(19, "Sophie Garnier", "sophie@test.com", "Admin", true, new DateTime(2023, 6, 5))
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