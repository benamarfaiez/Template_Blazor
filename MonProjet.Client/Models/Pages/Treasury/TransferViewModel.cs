using MonProjet.Shared.Models.Treasury;

namespace MonProjet.Client.Models.Pages.Treasury;

public class TransferViewModel
{
    public string Title { get; set; } = "Nouveau virement bancaire";
    public bool IsLoading { get; set; }
    public string? ErrorMessage { get; set; }

    // Données de référence
    public List<AccountDto> Accounts { get; set; } = [];
    public List<BeneficiaryDto> Beneficiaries { get; set; } = [];

    // Données de la transaction en cours
    public AccountDto? SelectedAccount { get; set; }
    public BeneficiaryDto? SelectedBeneficiary { get; set; }
    public decimal Amount { get; set; }
    public DateTime? ExecutionDate { get; set; } = DateTime.Today;
    public string Reason { get; set; } = string.Empty;
}