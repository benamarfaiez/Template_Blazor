using MonProjet.Client.Models.Pages.Treasury;
using MonProjet.Client.Services;
using MonProjet.Client.View.Treasury;

namespace MonProjet.Client.Presenters.Treasury;

public class TransferPresenter(IExceptionHandler exceptions, IToastService toastService) : ITransferPresenter
{
    private ITransferView? _view;
    private readonly IExceptionHandler _exceptions = exceptions;
    private readonly IToastService _toastService = toastService;

    public TransferViewModel ViewModel { get; } = new();

    public void SetView(ITransferView view) => _view = view;

    public async Task InitializeAsync()
    {
        ViewModel.IsLoading = true;
        try
        {
            await Task.Delay(300);
            ViewModel.Accounts =
            [
                new(1, "Compte Principal", "FR76 1234...", 15420.50m),
                new(2, "Compte Épargne", "FR76 9876...", 5000.00m)
            ];
            ViewModel.Beneficiaries =
            [
                new(1, "Fournisseur Alpha", "FR76 1111..."),
            ];
        }
        catch (Exception ex)
        {
            await _exceptions.HandleAsync(ex, "Erreur de chargement.");
            _view?.ShowError("Impossible de charger les données.");
        }
        finally
        {
            ViewModel.IsLoading = false;
        }
    }

    public async Task ExecuteTransferAsync()
    {
        try
        {
            ViewModel.IsLoading = true;
            await Task.Delay(1000);

            _toastService.ShowSuccess("Le virement a été exécuté avec succès.", "Transaction validée");

            // ✅ On appelle la méthode de l'interface de la Vue
            _view?.OnTransferCompleted();
        }
        catch (Exception ex)
        {
            await _exceptions.HandleAsync(ex, "Erreur d'exécution.");
            _view?.ShowError("Le virement a échoué.");
        }
        finally
        {
            ViewModel.IsLoading = false;
        }
    }

    // ✅ LOGIQUE MÉTIER : Validation des étapes (MVP)
    public Task<bool> ValidateStepAsync(int currentIndex, int nextIndex)
    {
        // Étape 1 -> 2 : Vérifier que le compte est sélectionné
        if (currentIndex == 0 && ViewModel.SelectedAccount is null)
        {
            _toastService.ShowWarning("Veuillez sélectionner un compte source.");
            return Task.FromResult(false);
        }

        // Étape 2 -> 3 : Vérifier que le bénéficiaire est sélectionné
        if (currentIndex == 1 && ViewModel.SelectedBeneficiary is null)
        {
            _toastService.ShowWarning("Veuillez sélectionner un bénéficiaire.");
            return Task.FromResult(false);
        }

        // Étape 3 -> 4 : Vérifier le montant, la date et le motif
        if (currentIndex == 2)
        {
            if (ViewModel.Amount <= 0)
            {
                _toastService.ShowWarning("Le montant doit être supérieur à zéro.");
                return Task.FromResult(false);
            }
            if (ViewModel.SelectedAccount != null && ViewModel.Amount > ViewModel.SelectedAccount.Balance)
            {
                _toastService.ShowError("Solde insuffisant sur le compte sélectionné.");
                return Task.FromResult(false);
            }
            if (string.IsNullOrWhiteSpace(ViewModel.Reason))
            {
                _toastService.ShowWarning("Veuillez saisir un motif de virement.");
                return Task.FromResult(false);
            }
        }

        return Task.FromResult(true);
    }
}