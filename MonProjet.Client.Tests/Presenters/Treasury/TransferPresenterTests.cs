using MonProjet.Client.Presenters.Treasury;
using MonProjet.Client.Services;
using MonProjet.Client.View.Treasury;
using MonProjet.Shared.Models.Treasury;
using NSubstitute;
using Xunit;

namespace MonProjet.Client.Tests.Presenters.Treasury;

[Trait("Category", "Unit")]
public sealed class TransferPresenterTests
{
    [Fact]
    public async Task ValidateStepAsync_WithoutSelectedAccount_ReturnsFalseAndWarns()
    {
        var toastService = Substitute.For<IToastService>();
        var presenter = new TransferPresenter(Substitute.For<IExceptionHandler>(), toastService);

        var result = await presenter.ValidateStepAsync(0, 1);

        Assert.False(result);
        toastService.Received(1).ShowWarning("Veuillez sélectionner un compte source.");
    }

    [Fact]
    public async Task ValidateStepAsync_AmountExceedsBalance_ReturnsFalseAndShowsError()
    {
        var toastService = Substitute.For<IToastService>();
        var presenter = new TransferPresenter(Substitute.For<IExceptionHandler>(), toastService);
        presenter.ViewModel.SelectedAccount = new AccountDto(1, "Compte", "FR00", 50m);
        presenter.ViewModel.Amount = 50.01m;
        presenter.ViewModel.Reason = "Facture";

        var result = await presenter.ValidateStepAsync(2, 3);

        Assert.False(result);
        toastService.Received(1).ShowError("Solde insuffisant sur le compte sélectionné.");
    }

    [Fact]
    public async Task ValidateStepAsync_ValidTransferDetails_ReturnsTrue()
    {
        var toastService = Substitute.For<IToastService>();
        var presenter = new TransferPresenter(Substitute.For<IExceptionHandler>(), toastService);
        presenter.ViewModel.SelectedAccount = new AccountDto(1, "Compte", "FR00", 50m);
        presenter.ViewModel.Amount = 50m;
        presenter.ViewModel.Reason = "Facture";

        var result = await presenter.ValidateStepAsync(2, 3);

        Assert.True(result);
        toastService.DidNotReceive().ShowWarning(Arg.Any<string>());
        toastService.DidNotReceive().ShowError(Arg.Any<string>());
    }

    [Fact]
    public async Task ExecuteTransferAsync_NotifiesViewAndShowsSuccess()
    {
        var toastService = Substitute.For<IToastService>();
        var view = Substitute.For<ITransferView>();
        var presenter = new TransferPresenter(Substitute.For<IExceptionHandler>(), toastService);
        presenter.SetView(view);

        await presenter.ExecuteTransferAsync();

        Assert.False(presenter.ViewModel.IsLoading);
        view.Received(1).OnTransferCompleted();
        toastService.Received(1).ShowSuccess("Le virement a été exécuté avec succès.", "Transaction validée");
    }
}
