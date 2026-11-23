using MonProjet.Client.Models.Pages.Treasury;
using MonProjet.Shared.Models.Treasury;
using Xunit;

namespace MonProjet.Client.Tests.Models.Pages.Treasury;

[Trait("Category", "Unit")]
public sealed class TransferViewModelTests
{
    [Fact]
    public void SetAccounts_SelfInput_PreservesExistingAccounts()
    {
        var viewModel = new TransferViewModel();
        AccountDto account = new(1, "Compte courant", "FR001", 500m);
        viewModel.SetAccounts([account]);

        viewModel.SetAccounts(viewModel.Accounts);

        Assert.Equal([account], viewModel.Accounts);
    }

    [Fact]
    public void SetAccounts_DeferredInput_PreservesEnumeratedAccounts()
    {
        var viewModel = new TransferViewModel();
        AccountDto account = new(1, "Compte courant", "FR001", 500m);
        viewModel.SetAccounts([account]);

        IEnumerable<AccountDto> deferredAccounts = viewModel.Accounts.Where(_ => true);
        viewModel.SetAccounts(deferredAccounts);

        Assert.Equal([account], viewModel.Accounts);
    }

    [Fact]
    public void SetAccounts_EmptyInput_ReplacesAccountsWithEmptyList()
    {
        var viewModel = new TransferViewModel();
        viewModel.SetAccounts([new(1, "Compte courant", "FR001", 500m)]);

        viewModel.SetAccounts([]);

        Assert.Empty(viewModel.Accounts);
    }

    [Fact]
    public void SetBeneficiaries_SelfInput_PreservesExistingBeneficiaries()
    {
        var viewModel = new TransferViewModel();
        BeneficiaryDto beneficiary = new(1, "Alice", "FR002");
        viewModel.SetBeneficiaries([beneficiary]);

        viewModel.SetBeneficiaries(viewModel.Beneficiaries);

        Assert.Equal([beneficiary], viewModel.Beneficiaries);
    }

    [Fact]
    public void SetBeneficiaries_DeferredInput_PreservesEnumeratedBeneficiaries()
    {
        var viewModel = new TransferViewModel();
        BeneficiaryDto beneficiary = new(1, "Alice", "FR002");
        viewModel.SetBeneficiaries([beneficiary]);

        IEnumerable<BeneficiaryDto> deferredBeneficiaries = viewModel.Beneficiaries.Where(_ => true);
        viewModel.SetBeneficiaries(deferredBeneficiaries);

        Assert.Equal([beneficiary], viewModel.Beneficiaries);
    }

    [Fact]
    public void SetBeneficiaries_EmptyInput_ReplacesBeneficiariesWithEmptyList()
    {
        var viewModel = new TransferViewModel();
        viewModel.SetBeneficiaries([new(1, "Alice", "FR002")]);

        viewModel.SetBeneficiaries([]);

        Assert.Empty(viewModel.Beneficiaries);
    }
}
