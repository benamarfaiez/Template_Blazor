using MonProjet.Client.Presenters.Users;
using MonProjet.Client.Services;
using MonProjet.Client.View.User;
using NSubstitute;
using Xunit;

namespace MonProjet.Client.Tests.Presenters.Users;

[Trait("Category", "Unit")]
public sealed class UserPresenterTests
{
    [Fact]
    public async Task LoadUsersAsync_LoadsUsersAndResetsLoading()
    {
        var exceptions = Substitute.For<IExceptionHandler>();
        var view = Substitute.For<IUserView>();
        var presenter = new UserPresenter(exceptions);
        presenter.SetView(view);

        await presenter.LoadUsersAsync();

        Assert.False(presenter.ViewModel.IsLoading);
        Assert.Equal(3, presenter.ViewModel.Users.Count);
        Assert.Null(presenter.ViewModel.ErrorMessage);
        view.Received(1).ShowLoading(true);
        view.Received(1).ShowLoading(false);
    }
}
