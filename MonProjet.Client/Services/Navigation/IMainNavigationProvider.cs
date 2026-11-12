using MonProjet.Client.Models.UI.Menu.NavBar;

namespace MonProjet.Client.Services.Navigation;

public interface IMainNavigationProvider
{
    IReadOnlyList<NavLinkItem> CreateLinks();
}
