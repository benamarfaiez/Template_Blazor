using MonProjet.Client.Components.UI.Core;
using MonProjet.Client.Models.UI.Menu;
using Microsoft.AspNetCore.Components;

namespace MonProjet.Client.Components.UI.Menu;

public partial class AppBreadcrumb : AppComponentBase
{
    [Parameter] public List<BreadcrumbItem> Items { get; set; } = [];

    /// <summary>
    /// Caractère ou icône utilisé pour séparer les éléments.
    /// </summary>
    [Parameter] public string Separator { get; set; } = "›";

    /// <summary>
    /// Si vrai, le dernier élément (page actuelle) est mis en évidence.
    /// </summary>
    [Parameter] public bool HighlightLastItem { get; set; } = true;
}