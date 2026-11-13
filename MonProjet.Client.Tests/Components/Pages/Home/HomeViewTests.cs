using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using MonProjet.Client.Components.Pages.Home;
using MonProjet.Client.Components.UI.Feedback;
using MonProjet.Client.Models.Pages.Home;
using MonProjet.Client.Presenters.Home;
using MonProjet.Client.Resources;
using MonProjet.Client.Services;
using MonProjet.Client.View.Home;
using MonProjet.Shared.Models.Home;
using NSubstitute;
using Xunit;

namespace MonProjet.Client.Tests.Components.Pages.Home;

public sealed class HomeViewTests
{
    [Fact]
    public void Render_RegistersViewInitializesPresenterAndDisplaysItems()
    {
        using var context = new BunitContext();
        var viewModel = new HomeViewModel
        {
            Items =
            [
                new HomeItemDto(1, "Élément A", "Actif", DateTime.Today),
                new HomeItemDto(2, "Élément B", "En attente", DateTime.Today)
            ]
        };
        var presenter = Substitute.For<IHomePresenter>();
        presenter.ViewModel.Returns(viewModel);
        presenter.InitializeAsync().Returns(Task.CompletedTask);
        presenter.RefreshDataAsync().Returns(Task.CompletedTask);
        context.Services.AddScoped<IHomePresenter>(_ => presenter);
        context.Services.AddSingleton(Substitute.For<ILanguageService>());
        var localizer = Substitute.For<IStringLocalizer<HomeResource>>();
        localizer[Arg.Any<string>()].Returns(call => new LocalizedString(call.Arg<string>(), call.Arg<string>()));
        context.Services.AddSingleton(localizer);

        var cut = context.Render<HomeView>();

        presenter.Received(1).SetView(Arg.Any<IHomeView>());
        presenter.Received(1).InitializeAsync();
        Assert.Equal(2, cut.FindAll(".app-datagrid tbody tr").Count);
        Assert.Contains("Élément A", cut.Markup);
    }

    [Fact]
    public void RefreshButton_Click_CallsPresenter()
    {
        using var context = new BunitContext();
        var presenter = Substitute.For<IHomePresenter>();
        presenter.ViewModel.Returns(new HomeViewModel());
        presenter.InitializeAsync().Returns(Task.CompletedTask);
        presenter.RefreshDataAsync().Returns(Task.CompletedTask);
        context.Services.AddScoped<IHomePresenter>(_ => presenter);
        context.Services.AddSingleton(Substitute.For<ILanguageService>());
        var localizer = Substitute.For<IStringLocalizer<HomeResource>>();
        localizer[Arg.Any<string>()].Returns(call => new LocalizedString(call.Arg<string>(), call.Arg<string>()));
        context.Services.AddSingleton(localizer);
        var cut = context.Render<HomeView>();

        cut.FindAll("button").Single(button => button.TextContent.Contains("Btn_Refresh", StringComparison.Ordinal)).Click();

        presenter.Received(1).RefreshDataAsync();
    }

    [Fact]
    public void IsLoading_RendersProgressRingInsteadOfGrid()
    {
        using var context = new BunitContext();
        var viewModel = new HomeViewModel { IsLoading = true };
        var presenter = Substitute.For<IHomePresenter>();
        presenter.ViewModel.Returns(viewModel);
        presenter.InitializeAsync().Returns(Task.CompletedTask);
        context.Services.AddScoped<IHomePresenter>(_ => presenter);
        context.Services.AddSingleton(Substitute.For<ILanguageService>());
        var localizer = Substitute.For<IStringLocalizer<HomeResource>>();
        localizer[Arg.Any<string>()].Returns(call => new LocalizedString(call.Arg<string>(), call.Arg<string>()));
        context.Services.AddSingleton(localizer);

        var cut = context.Render<HomeView>();

        Assert.NotNull(cut.FindComponent<AppProgressRing>());
        Assert.Empty(cut.FindAll(".app-datagrid tbody tr"));
    }
}