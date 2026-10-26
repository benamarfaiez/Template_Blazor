using Microsoft.AspNetCore.Components;
using MonProjet.Client.Components.UI.Core;
using MonProjet.Client.Components.UI.Layout.Helpers;

namespace MonProjet.Client.Components.UI.Layout;

public partial class AppSplitGrid : AppComponentBase
{
    [Parameter, EditorRequired] public RenderFragment? Left { get; set; }
    [Parameter, EditorRequired] public RenderFragment? Right { get; set; }

    // Le composant dépend désormais de l'abstraction SplitRatio
    [Parameter] public SplitRatio Ratio { get; set; } = SplitRatio.OneToTwo;

    [Parameter] public string Gap { get; set; } = "16px";

    private string _finalGridStyles = string.Empty;

    protected override void OnParametersSet()
    {
        // Délégation du calcul au Helper (DIP)
        _finalGridStyles = SplitGridStyleBuilder.Build(Ratio, Gap);
        base.OnParametersSet();
    }
}