using System.Collections.ObjectModel;
using MonProjet.Shared.Models.Treasury;

namespace MonProjet.Client.Models.Pages.Treasury;

public class TransferViewModel
{
    public string Title { get; set; } = "Nouveau virement bancaire";
    public bool IsLoading { get; set; }
    public string? ErrorMessage { get; set; }
    private readonly List<AccountDto> _accounts = [];
    private readonly List<BeneficiaryDto> _beneficiaries = [];

    public IReadOnlyList<AccountDto> Accounts => _accounts;
    public IReadOnlyList<BeneficiaryDto> Beneficiaries => _beneficiaries;

    public void SetAccounts(IEnumerable<AccountDto> accounts)
    {
        _accounts.Clear();
        _accounts.AddRange(accounts);
    }

    public void SetBeneficiaries(IEnumerable<BeneficiaryDto> beneficiaries)
    {
        _beneficiaries.Clear();
        _beneficiaries.AddRange(beneficiaries);
    }

    public AccountDto? SelectedAccount { get; set; }
    public BeneficiaryDto? SelectedBeneficiary { get; set; }
    public decimal Amount { get; set; }
    public DateTime? ExecutionDate { get; set; } = DateTime.Today;
    public string Reason { get; set; } = string.Empty;
}