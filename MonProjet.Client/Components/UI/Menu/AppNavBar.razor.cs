using Microsoft.AspNetCore.Components;
using MonProjet.Client.Components.UI.Core;
using MonProjet.Client.Models.NavBar;

namespace MonProjet.Client.Components.UI.Menu;

public partial class AppNavBar : AppComponentBase
{
    [Parameter] public string Brand { get; set; } = "MonProjet";
    [Parameter] public RenderFragment? BrandTemplate { get; set; }
    [Parameter] public List<NavLinkItem> Links { get; set; } = [];
    [Parameter] public RenderFragment? RightContent { get; set; }

    private NavLinkItem? _activeDropdown;
    private bool _isMobileMenuOpen;

    private void OnMouseEnter(NavLinkItem link)
    {
        if (link.HasDropdown)
        {
            _activeDropdown = link;
        }
    }

    private void OnMouseLeave()
    {
        _activeDropdown = null;
    }

    private void ToggleDropdown(NavLinkItem link)
    {
        if (!link.HasDropdown) return;
        _activeDropdown = _activeDropdown == link ? null : link;
    }

    private void CloseAll()
    {
        _activeDropdown = null;
        _isMobileMenuOpen = false;
    }

    private void ToggleMobileMenu()
    {
        _isMobileMenuOpen = !_isMobileMenuOpen;
        _activeDropdown = null;
    }
}