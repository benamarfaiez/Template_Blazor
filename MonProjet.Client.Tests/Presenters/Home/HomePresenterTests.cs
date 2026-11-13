using MonProjet.Client.Presenters.Home;
using MonProjet.Client.Services;
using MonProjet.Client.View.Home;
using NSubstitute;
using Xunit;

namespace MonProjet.Client.Tests.Presenters.Home;

[Trait("Category", "Unit")]
public sealed class HomePresenterTests
{
    [Fact]
    public async Task InitializeAsync_LoadsItemsAndResetsLoading()
    {
        var exceptions = Substitute.For<IExceptionHandler>();
        var view = Substitute.For<IHomeView>();
        var presenter = new HomePresenter(exceptions);
        presenter.SetView(view);

        await presenter.InitializeAsync();

        Assert.False(presenter.ViewModel.IsLoading);
        Assert.Equal(3, presenter.ViewModel.Items.Count);
        view.Received(1).ShowLoading(true);
        view.Received(1).ShowLoading(false);
    }

    [Fact]
    public async Task RefreshDataAsync_SearchIsCaseInsensitive()
    {
        var exceptions = Substitute.For<IExceptionHandler>();
        var view = Substitute.For<IHomeView>();
        view.SearchTerm.Returns("aLpHa");
        var presenter = new HomePresenter(exceptions);
        presenter.SetView(view);

        await presenter.RefreshDataAsync();

        var item = Assert.Single(presenter.ViewModel.Items);
        Assert.Equal("Projet Alpha", item.Name);
        Assert.False(presenter.ViewModel.IsLoading);
    }
}
