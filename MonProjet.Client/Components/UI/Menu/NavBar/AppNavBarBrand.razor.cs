using Microsoft.AspNetCore.Components;
using MonProjet.Client.Components.UI.Core;

namespace MonProjet.Client.Components.UI.Menu.NavBar;

public partial class AppNavBarBrand : AppComponentBase
{
    /// <summary>
    /// Le texte affiché par défaut (si aucun template n'est fourni).
    /// </summary>
    [Parameter] public string Brand { get; set; } = "MonProjet";

    /// <summary>
    /// L'URL de destination. Par défaut la racine.
    /// </summary>
    [Parameter] public string Href { get; set; } = "/";

    /// <summary>
    /// Cible du lien (ex: "_blank" pour ouvrir dans un nouvel onglet).
    /// </summary>
    [Parameter] public string? Target { get; set; }

    /// <summary>
    /// Label d'accessibilité pour les lecteurs d'écran. 
    /// Si vide, utilise la propriété Brand.
    /// </summary>
    [Parameter] public string? AriaLabel { get; set; }

    /// <summary>
    /// Permet d'injecter une image ou un SVG à la place du texte.
    /// </summary>
    [Parameter] public RenderFragment? BrandTemplate { get; set; }

    /// <summary>
    /// Événement déclenché au clic (utile pour fermer le menu mobile).
    /// </summary>
    [Parameter] public EventCallback OnClick { get; set; }

    private async Task HandleClickAsync()
    {
        if (OnClick.HasDelegate)
        {
            await OnClick.InvokeAsync();
        }
    }
}