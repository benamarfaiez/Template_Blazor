using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MonProjet.Client.Components.UI.Menu;

namespace MonProjet.Client.Components.Layout;

public partial class MainLayout : LayoutComponentBase
{
    private ErrorBoundary? _errorBoundary;

    // Définition des liens de navigation
    private readonly List<AppNavBar.NavLinkItem> _navLinks =
    [
        new() { Href = "/", Label = "Tableau de bord" },
        new() { Href = "/gallery", Label = "Galerie UI" },
        new() { Href = "/users", Label = "Utilisateurs" },
        new() { Href = "/employees", Label = "Employés" }
    ];

    private void Recover()
    {
        _errorBoundary?.Recover();
    }
}