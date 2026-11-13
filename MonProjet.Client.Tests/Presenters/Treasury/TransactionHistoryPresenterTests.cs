using MonProjet.Client.Models.UI.Date;
using MonProjet.Client.Presenters.Treasury;
using MonProjet.Client.Services;
using NSubstitute;
using Xunit;

namespace MonProjet.Client.Tests.Presenters.Treasury;

[Trait("Category", "Unit")]
public sealed class TransactionHistoryPresenterTests
{
    [Fact]
    public async Task InitializeAsync_LoadsTransactionsInDescendingDateOrder()
    {
        var presenter = new TransactionHistoryPresenter(
            Substitute.For<IExceptionHandler>(),
            Substitute.For<IToastService>());

        await presenter.InitializeAsync();

        Assert.Equal(DateRangePreset.Last30Days, presenter.ViewModel.CurrentPreset);
        Assert.Equal(30, (presenter.ViewModel.CurrentDateRange.End!.Value - presenter.ViewModel.CurrentDateRange.Start!.Value).Days);
        Assert.True(presenter.ViewModel.Transactions
            .Zip(presenter.ViewModel.Transactions.Skip(1), (current, next) => current.Date >= next.Date)
            .All(isDescending => isDescending));
        Assert.False(presenter.ViewModel.IsLoading);
    }

    [Fact]
    public async Task FilterByDateRangeAsync_FutureRangeReturnsNoTransactions()
    {
        var view = Substitute.For<ITransactionHistoryView>();
        var presenter = new TransactionHistoryPresenter(
            Substitute.For<IExceptionHandler>(),
            Substitute.For<IToastService>());
        presenter.SetView(view);

        await presenter.FilterByDateRangeAsync(new DateRange(DateTime.Today.AddYears(1), DateTime.Today.AddYears(1).AddDays(1)));

        Assert.Empty(presenter.ViewModel.Transactions);
        Assert.False(presenter.ViewModel.IsLoading);
        view.Received(1).ShowLoading(true);
        view.Received(1).ShowLoading(false);
    }
}
