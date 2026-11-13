using Bunit;
using Microsoft.Extensions.DependencyInjection;
using MonProjet.Client.Components.Pages.Treasury;
using MonProjet.Client.Components.UI.BasicInput;
using MonProjet.Client.Components.UI.Feedback;
using MonProjet.Client.Models.Pages.Treasury;
using MonProjet.Client.Models.UI.Date;
using MonProjet.Client.Presenters.Treasury;
using MonProjet.Shared.Models.Treasury;
using NSubstitute;
using Xunit;

namespace MonProjet.Client.Tests.Components.Pages.Treasury;

public sealed class TransactionHistoryViewTests
{
    [Fact]
    public void Render_RegistersViewInitializesPresenterAndDisplaysTransactions()
    {
        using var context = new BunitContext();
        var viewModel = new TransactionHistoryViewModel
        {
            Transactions = [new TransactionDto(Guid.NewGuid(), DateTime.Today, "Virement test", 125m, true)]
        };
        var presenter = Substitute.For<ITransactionHistoryPresenter>();
        presenter.ViewModel.Returns(viewModel);
        presenter.InitializeAsync().Returns(Task.CompletedTask);
        presenter.FilterByDateRangeAsync(Arg.Any<DateRange>()).Returns(Task.CompletedTask);
        context.Services.AddScoped<ITransactionHistoryPresenter>(_ => presenter);

        var cut = context.Render<TransactionHistoryView>();

        presenter.Received(1).SetView(Arg.Any<ITransactionHistoryView>());
        presenter.Received(1).InitializeAsync();
        Assert.Single(cut.FindAll(".app-datagrid tbody tr"));
        Assert.Contains("Virement test", cut.Markup);
    }

    [Fact]
    public async Task DateRangeChanged_ForwardsRangeToPresenter()
    {
        using var context = new BunitContext();
        var presenter = Substitute.For<ITransactionHistoryPresenter>();
        presenter.ViewModel.Returns(new TransactionHistoryViewModel());
        presenter.InitializeAsync().Returns(Task.CompletedTask);
        presenter.FilterByDateRangeAsync(Arg.Any<DateRange>()).Returns(Task.CompletedTask);
        context.Services.AddScoped<ITransactionHistoryPresenter>(_ => presenter);
        var cut = context.Render<TransactionHistoryView>();
        var range = new DateRange(DateTime.Today.AddDays(-7), DateTime.Today);

        await cut.InvokeAsync(() => cut.FindComponent<AppDateRangePicker>().Instance.OnRangeChanged.InvokeAsync(range));

        await presenter.Received(1).FilterByDateRangeAsync(range);
    }

    [Fact]
    public void IsLoading_RendersProgressRingInsteadOfTransactionsGrid()
    {
        using var context = new BunitContext();
        var presenter = Substitute.For<ITransactionHistoryPresenter>();
        presenter.ViewModel.Returns(new TransactionHistoryViewModel { IsLoading = true });
        presenter.InitializeAsync().Returns(Task.CompletedTask);
        presenter.FilterByDateRangeAsync(Arg.Any<DateRange>()).Returns(Task.CompletedTask);
        context.Services.AddScoped<ITransactionHistoryPresenter>(_ => presenter);

        var cut = context.Render<TransactionHistoryView>();

        Assert.NotNull(cut.FindComponent<AppProgressRing>());
        Assert.Empty(cut.FindAll(".app-datagrid tbody tr"));
    }
}