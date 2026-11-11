using Microsoft.AspNetCore.Components;
using MonProjet.Client.Components.UI.Core;
using MonProjet.Client.Models.UI.Menu.NavBar;

namespace MonProjet.Client.Components.UI.Menu.NavBar;

public partial class AppNavBarLinks : AppComponentBase
{
    [Parameter]
    public List<NavLinkItem> Links { get; set; } = [];

    /// <summary>
    /// Indique si le menu mobile est ouvert (géré par le parent AppNavBar).
    /// </summary>
    [Parameter]
    public bool IsMobileMenuOpen { get; set; }

    /// <summary>
    /// Le lien actif (qui a son méga-menu ouvert).
    /// </summary>
    [Parameter]
    public NavLinkItem? ActiveDropdown { get; set; }

    /// <summary>
    /// Déclenché au clic sur un lien classique (pour fermer le menu mobile).
    /// </summary>
    [Parameter]
    public EventCallback OnLinkClick { get; set; }

    /// <summary>
    /// Déclenché au clic sur un menu déroulant.
    /// </summary>
    [Parameter]
    public EventCallback<NavLinkItem> OnDropdownToggle { get; set; }
    [Parameter] public EventCallback<NavLinkItem> OnMouseEnter { get; set; }
    [Parameter] public EventCallback OnMouseLeave { get; set; }
    [Parameter] public RenderFragment? DropdownContent { get; set; }
    private bool IsDropdownActive(NavLinkItem link)
    {
        return ActiveDropdown == link;
    }
}