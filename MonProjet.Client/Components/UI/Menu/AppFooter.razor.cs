using Microsoft.AspNetCore.Components;
using MonProjet.Client.Components.UI.Core;

namespace MonProjet.Client.Components.UI.Menu;

public partial class AppFooter : AppComponentBase
{
    /// <summary>
    /// Nom de l'entreprise ou de l'application.
    /// </summary>
    [Parameter] public string BrandName { get; set; } = "MonProjet";

    /// <summary>
    /// Année affichée dans le copyright. Si non fournie, utilise l'année en cours.
    /// </summary>
    [Parameter] public int? Year { get; set; }

    /// <summary>
    /// Permet d'afficher des liens (ex: Mentions légales, CGU) au centre du footer.
    /// </summary>
    [Parameter] public RenderFragment? ChildContent { get; set; }

    // ✅ BUGFIX : L'année est dynamique, elle ne sera plus fausse en 2027
    private int CurrentYear => Year ?? DateTime.Now.Year;
}