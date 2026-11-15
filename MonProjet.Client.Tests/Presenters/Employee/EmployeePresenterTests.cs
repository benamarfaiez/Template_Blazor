using MonProjet.Client.Presenters.Employee;
using MonProjet.Client.Services;
using MonProjet.Client.View.Employee;
using NSubstitute;
using Xunit;

namespace MonProjet.Client.Tests.Presenters.Employee;

[Trait("Category", "Unit")]
public sealed class EmployeePresenterTests
{
    [Fact]
    public async Task LoadEmployeesAsync_LoadsEmployeesAndResetsLoading()
    {
        var exceptions = Substitute.For<IExceptionHandler>();
        var toastService = Substitute.For<IToastService>();
        var view = Substitute.For<IEmployeeView>();
        var presenter = new EmployeePresenter(exceptions, toastService);
        presenter.SetView(view);

        await presenter.LoadEmployeesAsync();

        Assert.False(presenter.ViewModel.IsLoading);
        Assert.Equal(19, presenter.ViewModel.Employees.Count);
        view.Received(1).ShowLoading(true);
        view.Received(1).ShowLoading(false);
    }

    [Fact]
    public async Task SaveEmployeeAsync_ShowsSuccessNotification()
    {
        var exceptions = Substitute.For<IExceptionHandler>();
        var toastService = Substitute.For<IToastService>();
        var presenter = new EmployeePresenter(exceptions, toastService);

        await presenter.SaveEmployeeAsync();

        toastService.Received(1).ShowSuccess("Employé enregistré avec succès.");
    }
}
