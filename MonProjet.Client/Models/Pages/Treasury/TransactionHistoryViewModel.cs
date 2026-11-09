using MonProjet.Client.Models.UI.Date;
using MonProjet.Shared.Models.Treasury;

namespace MonProjet.Client.Models.Pages.Treasury;

public class TransactionHistoryViewModel
{
    public string Title { get; set; } = "Historique des Transactions";
    public bool IsLoading { get; set; }
    public string? ErrorMessage { get; set; }
    public DateRange CurrentDateRange { get; set; } = new(null, null);
    public List<TransactionDto> Transactions { get; set; } = [];
}