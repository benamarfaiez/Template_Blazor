using MonProjet.Client.Models.Home;
using MonProjet.Client.Presenters.Common;
using MonProjet.Client.View.Home;

namespace MonProjet.Client.Presenters.Home;

public interface IHomePresenter : IPresenter<IHomeView>
{
    HomeViewModel ViewModel { get; }
    Task InitializeAsync();
    Task RefreshDataAsync();
}
