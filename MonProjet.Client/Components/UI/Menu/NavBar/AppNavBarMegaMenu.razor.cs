using Microsoft.AspNetCore.Components;
using MonProjet.Client.Components.UI.Core;
using MonProjet.Client.Models.UI.Menu.NavBar;

namespace MonProjet.Client.Components.UI.Menu.NavBar;

public partial class AppNavBarMegaMenu : AppComponentBase
{
    [Parameter] public NavLinkItem? ActiveLink { get; set; }

    /// <summary>
    /// Événement déclenché quand l'utilisateur clique sur un lien dans le méga-menu.
    /// Permet au parent (AppNavBar) de fermer le menu automatiquement.
    /// </summary>
    [Parameter] public EventCallback OnLinkClick { get; set; }

    private async Task HandleLinkClickAsync()
    {
        if (OnLinkClick.HasDelegate)
        {
            await OnLinkClick.InvokeAsync();
        }
    }
}