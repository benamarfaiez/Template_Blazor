using Bunit;
using Microsoft.Extensions.DependencyInjection;
using MonProjet.Client.Components.Pages.Treasury;
using MonProjet.Client.Components.UI.Feedback;
using MonProjet.Client.Models.Pages.Treasury;
using MonProjet.Client.Presenters.Treasury;
using MonProjet.Client.Services;
using MonProjet.Client.View.Treasury;
using MonProjet.Shared.Models.Treasury;
using NSubstitute;
using Xunit;

namespace MonProjet.Client.Tests.Components.Pages.Treasury;

public sealed class TransferViewTests
{
    [Fact]
    public void Render_RegistersViewInitializesPresenterAndDisplaysViewModel()
    {
        using var context = new BunitContext();
        var viewModel = new TransferViewModel
        {
            Title = "Virement de test",
            Accounts = [new AccountDto(1, "Compte courant", "FR001", 500m)],
            Beneficiaries = [new BeneficiaryDto(2, "Alice", "FR002")]
        };
        var presenter = Substitute.For<ITransferPresenter>();
        presenter.ViewModel.Returns(viewModel);
        presenter.InitializeAsync().Returns(Task.CompletedTask);
        presenter.ValidateStepAsync(Arg.Any<int>(), Arg.Any<int>()).Returns(true);
        presenter.ExecuteTransferAsync().Returns(Task.CompletedTask);
        context.Services.AddScoped<ITransferPresenter>(_ => presenter);
        context.Services.AddSingleton(Substitute.For<IToastService>());

        var cut = context.Render<TransferView>();

        presenter.Received(1).SetView(Arg.Any<ITransferView>());
        presenter.Received(1).InitializeAsync();
        Assert.Contains("Virement de test", cut.Markup);
        Assert.Contains("Compte courant", cut.Markup);
    }

    [Fact]
    public void NextStep_Click_DelegatesValidationToPresenter()
    {
        using var context = new BunitContext();
        var presenter = Substitute.For<ITransferPresenter>();
        presenter.ViewModel.Returns(new TransferViewModel());
        presenter.InitializeAsync().Returns(Task.CompletedTask);
        presenter.ValidateStepAsync(Arg.Any<int>(), Arg.Any<int>()).Returns(true);
        presenter.ExecuteTransferAsync().Returns(Task.CompletedTask);
        context.Services.AddScoped<ITransferPresenter>(_ => presenter);
        context.Services.AddSingleton(Substitute.For<IToastService>());
        var cut = context.Render<TransferView>();

        cut.FindAll("button").Single(button => button.TextContent.Contains("Suivant", StringComparison.Ordinal)).Click();

        presenter.Received(1).ValidateStepAsync(0, 1);
    }

    [Fact]
    public void IsLoading_RendersProgressRingInsteadOfTransferStepper()
    {
        using var context = new BunitContext();
        var presenter = Substitute.For<ITransferPresenter>();
        presenter.ViewModel.Returns(new TransferViewModel { IsLoading = true });
        presenter.InitializeAsync().Returns(Task.CompletedTask);
        presenter.ValidateStepAsync(Arg.Any<int>(), Arg.Any<int>()).Returns(true);
        presenter.ExecuteTransferAsync().Returns(Task.CompletedTask);
        context.Services.AddScoped<ITransferPresenter>(_ => presenter);
        context.Services.AddSingleton(Substitute.For<IToastService>());

        var cut = context.Render<TransferView>();

        Assert.NotNull(cut.FindComponent<AppProgressRing>());
        Assert.DoesNotContain("app-stepper", cut.Markup);
    }
}