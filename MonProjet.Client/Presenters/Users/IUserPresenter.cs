using MonProjet.Client.Models.Pages.User;
using MonProjet.Client.Presenters.Common;
using MonProjet.Client.View.User;

namespace MonProjet.Client.Presenters.Users;

public interface IUserPresenter : IPresenter<IUserView>
{
    UserViewModel ViewModel { get; }
    Task InitializeAsync();
    Task LoadUsersAsync();
}
