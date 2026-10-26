using Microsoft.AspNetCore.Components;
using MonProjet.Client.Components.UI.Core;
using MonProjet.Client.Components.UI.Layout.Enums;
using MonProjet.Client.Components.UI.Layout.Helpers;

namespace MonProjet.Client.Components.UI.Layout;

public partial class AppStackPanel : AppComponentBase
{
    [Parameter] public StackOrientation Orientation { get; set; } = StackOrientation.Vertical;
    [Parameter] public int Gap { get; set; } = 8;
    [Parameter] public StackAlignment MainAxisAlignment { get; set; } = StackAlignment.Start;
    [Parameter] public StackAlignment CrossAxisAlignment { get; set; } = StackAlignment.Stretch;

    [Parameter] public RenderFragment? ChildContent { get; set; }

    private string _finalFlexStyles = string.Empty;

    protected override void OnParametersSet()
    {
        _finalFlexStyles = FlexStyleBuilder.Build(Orientation, Gap, MainAxisAlignment, CrossAxisAlignment);
        base.OnParametersSet();
    }
}