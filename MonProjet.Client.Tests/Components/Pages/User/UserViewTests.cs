using Bunit;
using Microsoft.Extensions.DependencyInjection;
using MonProjet.Client.Components.Pages.User;
using MonProjet.Client.Components.UI.Feedback;
using MonProjet.Client.Models.Pages.User;
using MonProjet.Client.Presenters.Users;
using MonProjet.Client.View.User;
using NSubstitute;
using Xunit;

namespace MonProjet.Client.Tests.Components.Pages.User;

public sealed class UserViewTests
{
    [Fact]
    public void Render_RegistersViewInitializesPresenterAndDisplaysUsers()
    {
        using var context = new BunitContext();
        var viewModel = new UserViewModel { Users = UserGridConfig.GetSampleUsers()[..3] };
        var presenter = Substitute.For<IUserPresenter>();
        presenter.ViewModel.Returns(viewModel);
        presenter.InitializeAsync().Returns(Task.CompletedTask);
        presenter.LoadUsersAsync().Returns(Task.CompletedTask);
        context.Services.AddScoped<IUserPresenter>(_ => presenter);

        var cut = context.Render<UserView>();

        presenter.Received(1).SetView(Arg.Any<IUserView>());
        presenter.Received(1).InitializeAsync();
        Assert.Equal(3, cut.FindAll(".app-datagrid tbody tr").Count);
        Assert.Contains("Alice", cut.Markup);
    }

    [Fact]
    public void ReloadButton_Click_CallsPresenter()
    {
        using var context = new BunitContext();
        var presenter = Substitute.For<IUserPresenter>();
        presenter.ViewModel.Returns(new UserViewModel());
        presenter.InitializeAsync().Returns(Task.CompletedTask);
        presenter.LoadUsersAsync().Returns(Task.CompletedTask);
        context.Services.AddScoped<IUserPresenter>(_ => presenter);
        var cut = context.Render<UserView>();

        cut.FindAll("button").Single(button => button.TextContent.Contains("Recharger la liste", StringComparison.Ordinal)).Click();

        presenter.Received(1).LoadUsersAsync();
    }

    [Fact]
    public void IsLoading_RendersProgressRingInsteadOfUserGrid()
    {
        using var context = new BunitContext();
        var presenter = Substitute.For<IUserPresenter>();
        presenter.ViewModel.Returns(new UserViewModel { IsLoading = true });
        presenter.InitializeAsync().Returns(Task.CompletedTask);
        presenter.LoadUsersAsync().Returns(Task.CompletedTask);
        context.Services.AddScoped<IUserPresenter>(_ => presenter);

        var cut = context.Render<UserView>();

        Assert.NotNull(cut.FindComponent<AppProgressRing>());
        Assert.Empty(cut.FindAll(".app-datagrid tbody tr"));
    }
}