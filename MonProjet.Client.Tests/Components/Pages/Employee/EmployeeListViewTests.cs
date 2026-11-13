using Bunit;
using Microsoft.Extensions.DependencyInjection;
using MonProjet.Client.Components.Pages.Employee;
using MonProjet.Client.Components.UI.Feedback;
using MonProjet.Client.Models.Pages.Employee;
using MonProjet.Client.Presenters.Employee;
using MonProjet.Client.View.Employee;
using MonProjet.Shared.Models.Employee;
using NSubstitute;
using Xunit;

namespace MonProjet.Client.Tests.Components.Pages.Employee;

public sealed class EmployeeListViewTests
{
    [Fact]
    public void Render_RegistersViewInitializesPresenterAndDisplaysEmployees()
    {
        using var context = new BunitContext();
        var viewModel = new EmployeeViewModel
        {
            Employees =
            [
                new EmployeeDto(1, "Alice Martin", "alice@example.com", "Admin", true, DateTime.Today),
                new EmployeeDto(2, "Bob Dupont", "bob@example.com", "Éditeur", false, DateTime.Today)
            ]
        };
        var presenter = Substitute.For<IEmployeePresenter>();
        presenter.ViewModel.Returns(viewModel);
        presenter.InitializeAsync().Returns(Task.CompletedTask);
        presenter.LoadEmployeesAsync().Returns(Task.CompletedTask);
        presenter.SaveEmployeeAsync().Returns(Task.CompletedTask);
        context.Services.AddScoped<IEmployeePresenter>(_ => presenter);

        var cut = context.Render<EmployeeListView>();

        presenter.Received(1).SetView(Arg.Any<IEmployeeView>());
        presenter.Received(1).InitializeAsync();
        Assert.Equal(2, cut.FindAll(".app-datagrid tbody tr").Count);
        Assert.Contains("Alice Martin", cut.Markup);
    }

    [Fact]
    public void ReloadButton_Click_CallsPresenter()
    {
        using var context = new BunitContext();
        var presenter = Substitute.For<IEmployeePresenter>();
        presenter.ViewModel.Returns(new EmployeeViewModel());
        presenter.InitializeAsync().Returns(Task.CompletedTask);
        presenter.LoadEmployeesAsync().Returns(Task.CompletedTask);
        presenter.SaveEmployeeAsync().Returns(Task.CompletedTask);
        context.Services.AddScoped<IEmployeePresenter>(_ => presenter);
        var cut = context.Render<EmployeeListView>();

        cut.FindAll("button").Single(button => button.TextContent.Contains("Recharger", StringComparison.Ordinal)).Click();

        presenter.Received(1).LoadEmployeesAsync();
    }

    [Fact]
    public void IsLoading_RendersProgressRingInsteadOfEmployeeGrid()
    {
        using var context = new BunitContext();
        var presenter = Substitute.For<IEmployeePresenter>();
        presenter.ViewModel.Returns(new EmployeeViewModel { IsLoading = true });
        presenter.InitializeAsync().Returns(Task.CompletedTask);
        presenter.LoadEmployeesAsync().Returns(Task.CompletedTask);
        presenter.SaveEmployeeAsync().Returns(Task.CompletedTask);
        context.Services.AddScoped<IEmployeePresenter>(_ => presenter);

        var cut = context.Render<EmployeeListView>();

        Assert.NotNull(cut.FindComponent<AppProgressRing>());
        Assert.Empty(cut.FindAll(".app-datagrid tbody tr"));
    }
}