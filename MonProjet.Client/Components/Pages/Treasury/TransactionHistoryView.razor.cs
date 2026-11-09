using Microsoft.AspNetCore.Components;
using MonProjet.Client.Components.UI.Collections.Column;
using MonProjet.Client.Components.UI.Helpers;
using MonProjet.Client.Models.UI.Date;
using MonProjet.Client.Presenters.Treasury;
using MonProjet.Shared.Models.Treasury;

namespace MonProjet.Client.Components.Pages.Treasury;

public partial class TransactionHistoryView : OwningComponentBase, ITransactionHistoryView, IDisposable
{
    private ITransactionHistoryPresenter Presenter => ScopedServices.GetRequiredService<ITransactionHistoryPresenter>();

    private List<GridColumn<TransactionDto>> _columns = null!;

    protected override void OnInitialized()
    {
        _columns = CreateColumns();
    }

    protected override async Task OnInitializedAsync()
    {
        Presenter.SetView(this);
        await Presenter.InitializeAsync();
    }

    private async Task HandleRangeChanged(DateRange newRange)
    {
        await Presenter.FilterByDateRangeAsync(newRange);
    }

    public void ShowError(string message) => StateHasChanged();
    public void ShowLoading(bool isLoading) => StateHasChanged();

    private static List<GridColumn<TransactionDto>> CreateColumns()
    {
        var columns = new List<GridColumn<TransactionDto>>
        {
            GridColumn.Text<TransactionDto>("Date", t => t.Date, comparer: (a, b) => a.Date.CompareTo(b.Date), formatter: raw => ((DateTime)raw!).ToString("dd/MM/yyyy")),
            GridColumn.Text<TransactionDto>("Libellé", t => t.Label),
            GridColumn.Template<TransactionDto>("Montant", t => builder =>
            {
                var color = t.IsIncome ? "var(--state-success-text)" : "var(--state-error-text)";
                var sign = t.IsIncome ? "+" : "-";
                builder.OpenElement(0, "span");
                builder.AddAttribute(1, "style", $"color: {color}; font-weight: 600;");
                builder.AddContent(2, $"{sign} {t.Amount:C}");
                builder.CloseElement();
            })
        };
        return columns;
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}