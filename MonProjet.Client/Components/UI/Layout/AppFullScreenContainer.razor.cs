using Microsoft.AspNetCore.Components;
using MonProjet.Client.Components.UI.Core;

namespace MonProjet.Client.Components.UI.Layout;

public partial class AppFullScreenContainer : AppComponentBase
{
    /// <summary>
    /// Événement pour notifier le parent de la fermeture (permet le @bind-IsVisible).
    /// </summary>
    [Parameter] public EventCallback<bool> IsVisibleChanged { get; set; }

    /// <summary>
    /// Titre affiché dans l'en-tête.
    /// </summary>
    [Parameter] public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Contenu principal de la page plein écran.
    /// </summary>
    [Parameter] public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Actions supplémentaires à afficher dans l'en-tête (à côté du bouton Fermer).
    /// </summary>
    [Parameter] public RenderFragment? HeaderActions { get; set; }

    /// <summary>
    /// Si vrai, affiche le bouton de fermeture (par défaut : vrai).
    /// </summary>
    [Parameter] public bool ShowCloseButton { get; set; } = true;

    private async Task CloseAsync()
    {
        IsVisible = false;
        await IsVisibleChanged.InvokeAsync(false);
    }
}