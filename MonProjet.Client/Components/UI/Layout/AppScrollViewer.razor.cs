using Microsoft.AspNetCore.Components;
using MonProjet.Client.Components.UI.Core;
using MonProjet.Client.Components.UI.Enums;

namespace MonProjet.Client.Components.UI.Layout;

public partial class AppScrollViewer : AppComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Hauteur maximale du conteneur avant que le défilement ne s'active.
    /// </summary>
    [Parameter] public string MaxHeight { get; set; } = "400px";

    /// <summary>
    /// Direction du défilement.
    /// </summary>
    [Parameter] public ScrollDirection Direction { get; set; } = ScrollDirection.Vertical;

    /// <summary>
    /// Active le défilement fluide (smooth) lors de la navigation ou du scroll programmatique.
    /// </summary>
    [Parameter] public bool SmoothScroll { get; set; } = true;

    private string DirectionStyle => Direction switch
    {
        ScrollDirection.Horizontal => "overflow-x: auto; overflow-y: hidden;",
        ScrollDirection.Both => "overflow: auto;",
        _ => "overflow-y: auto; overflow-x: hidden;"
    };

    private string FinalStyles => $"--scroll-max-height: {MaxHeight}; {DirectionStyle}";
    private string SmoothClass => SmoothScroll ? "is-smooth" : "";
}