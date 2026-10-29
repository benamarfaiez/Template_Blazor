using Microsoft.AspNetCore.Components;
using MonProjet.Client.Components.UI.Core;

namespace MonProjet.Client.Components.UI.Collections;

public partial class AppListBox<TItem> : AppComponentBase
{
    [Parameter] public IEnumerable<TItem>? Items { get; set; }
    [Parameter] public TItem? SelectedItem { get; set; }
    [Parameter] public EventCallback<TItem?> SelectedItemChanged { get; set; }

    /// <summary>
    /// Fonction pour extraire le texte à afficher pour chaque élément.
    /// </summary>
    [Parameter] public Func<TItem, string> LabelSelector { get; set; } = i => i?.ToString() ?? "";

    /// <summary>
    /// Template personnalisé si aucun élément n'est présent.
    /// </summary>
    [Parameter] public RenderFragment? EmptyTemplate { get; set; }

    private async Task SelectItemAsync(TItem item)
    {
        SelectedItem = item;
        await SelectedItemChanged.InvokeAsync(item);
    }
}