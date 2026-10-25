using MonProjet.Client.Presenters.Common;

namespace MonProjet.Client.View.Home;

public interface IHomeView : IView
{
    string SearchTerm { get; }
    event Action? OnRefreshRequested;
}
