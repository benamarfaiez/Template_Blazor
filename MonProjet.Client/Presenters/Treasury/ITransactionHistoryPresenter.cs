using MonProjet.Client.Models.Pages.Treasury;
using MonProjet.Client.Models.UI.Date;
using MonProjet.Client.Presenters.Common;

namespace MonProjet.Client.Presenters.Treasury;

public interface ITransactionHistoryPresenter : IPresenter<ITransactionHistoryView>
{
    TransactionHistoryViewModel ViewModel { get; }
    Task InitializeAsync();
    Task FilterByDateRangeAsync(DateRange range);
}

public interface ITransactionHistoryView : IView { }
