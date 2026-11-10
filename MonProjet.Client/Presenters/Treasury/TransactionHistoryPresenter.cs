using MonProjet.Client.Models.Pages.Treasury;
using MonProjet.Client.Models.UI.Date;
using MonProjet.Client.Services;
using MonProjet.Shared.Models.Treasury;

namespace MonProjet.Client.Presenters.Treasury;

public class TransactionHistoryPresenter(IExceptionHandler exceptions, IToastService toastService) : ITransactionHistoryPresenter, IDisposable
{
    private ITransactionHistoryView? _view;
    private readonly IExceptionHandler _exceptions = exceptions;
    private readonly IToastService _toastService = toastService;
    private CancellationTokenSource? _filterCts;
    private readonly List<TransactionDto> _allTransactions = GenerateSampleData();

    public TransactionHistoryViewModel ViewModel { get; } = new();

    public void SetView(ITransactionHistoryView view) => _view = view;

    public async Task InitializeAsync()
    {
        ViewModel.CurrentDateRange = new DateRange(DateTime.Today.AddDays(-30), DateTime.Today);
        await FilterByDateRangeAsync(ViewModel.CurrentDateRange);
    }

    public async Task FilterByDateRangeAsync(DateRange range)
    {
        ViewModel.CurrentDateRange = range;
        ViewModel.ErrorMessage = null;
        ViewModel.IsLoading = true;
        _view?.ShowLoading(true);

        _filterCts?.Cancel();
        _filterCts?.Dispose();
        _filterCts = new CancellationTokenSource();
        var token = _filterCts.Token;
        try
        {
            // Simulation d'un appel API
            await Task.Delay(400, token);

            var filtered = _allTransactions.AsEnumerable();

            if (range.Start.HasValue)
                filtered = filtered.Where(t => t.Date.Date >= range.Start.Value.Date);

            if (range.End.HasValue)
                filtered = filtered.Where(t => t.Date.Date <= range.End.Value.Date);

            token.ThrowIfCancellationRequested();

            ViewModel.Transactions = [.. filtered.OrderByDescending(t => t.Date)];

            if (ViewModel.Transactions.Count == 0)
            {
                _toastService.ShowInfo("Aucune transaction trouvée pour cette période.");
            }
        }
        catch (Exception ex)
        {
            await _exceptions.HandleAsync(ex, "Erreur lors du filtrage.");
            ViewModel.ErrorMessage = "Impossible de filtrer les transactions.";
            _view?.ShowError(ViewModel.ErrorMessage);
        }
        finally
        {
            ViewModel.IsLoading = false;
            _view?.ShowLoading(false);
        }
    }

    private static List<TransactionDto> GenerateSampleData()
    {
        var random = new Random();
        return [.. Enumerable.Range(1, 50).Select(i => new TransactionDto(
            Guid.NewGuid(),
            DateTime.Today.AddDays(-random.Next(0, 90)), // Transactions sur les 90 derniers jours
            i % 2 == 0 ? "Facture Fournisseur" : "Virement Client",
            Math.Round((decimal)(random.NextDouble() * 1000), 2),
            i % 2 != 0
        ))];
    }

    public void Dispose()
    {
        _filterCts?.Cancel();
        _filterCts?.Dispose();
        GC.SuppressFinalize(this);
    }
}