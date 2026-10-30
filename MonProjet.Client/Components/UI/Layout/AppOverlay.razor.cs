using Microsoft.AspNetCore.Components;
using MonProjet.Client.Components.UI.Core;

namespace MonProjet.Client.Components.UI.Layout;

public partial class AppOverlay : AppComponentBase
{
    /// <summary>
    /// Permet la liaison bidirectionnelle (@bind-IsVisible).
    /// </summary>
    [Parameter] public EventCallback<bool> IsVisibleChanged { get; set; }

    /// <summary>
    /// Contenu affiché à l'intérieur de l'overlay.
    /// </summary>
    [Parameter] public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Si vrai, un clic sur le fond sombre (en dehors du contenu) ferme l'overlay.
    /// </summary>
    [Parameter] public bool CloseOnOverlay { get; set; } = true;

    /// <summary>
    /// Active le piège à focus (empêche l'utilisateur de tabuler derrière l'overlay).
    /// </summary>
    [Parameter] public bool TrapFocus { get; set; } = true;

    private async Task CloseAsync()
    {
        IsVisible = false;
        await IsVisibleChanged.InvokeAsync(false);
    }

    private async Task OnOverlayClickAsync()
    {
        if (CloseOnOverlay)
        {
            await CloseAsync();
        }
    }
}