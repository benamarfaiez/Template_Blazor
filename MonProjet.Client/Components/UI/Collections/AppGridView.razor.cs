using Microsoft.AspNetCore.Components;
using MonProjet.Client.Components.UI.Core;

namespace MonProjet.Client.Components.UI.Collections;

public partial class AppGridView<TItem> : AppComponentBase
{
    [Parameter] public IEnumerable<TItem>? Items { get; set; }
    [Parameter] public RenderFragment<TItem>? ItemTemplate { get; set; }
    [Parameter] public RenderFragment? EmptyTemplate { get; set; }

    /// <summary>
    /// Nombre de colonnes sur grand écran (par défaut : 3).
    /// </summary>
    [Parameter] public int Columns { get; set; } = 3;

    /// <summary>
    /// Espacement entre les cartes (par défaut : 16px).
    /// </summary>
    [Parameter] public string Gap { get; set; } = "16px";

    // Génération des variables CSS pour un style plus propre et modifiable
    private string GridStyles => $"--grid-columns: {Columns}; --grid-gap: {Gap};";
}