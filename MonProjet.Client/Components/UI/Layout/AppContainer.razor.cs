using Microsoft.AspNetCore.Components;
using MonProjet.Client.Components.UI.Core;
using MonProjet.Client.Components.UI.Enums;
using MonProjet.Client.Components.UI.Helpers;

namespace MonProjet.Client.Components.UI.Layout;

public partial class AppContainer : AppComponentBase
{
    [Parameter] public ContainerAlignment HorizontalAlignment { get; set; } = ContainerAlignment.Stretch;
    [Parameter] public ContainerAlignment VerticalAlignment { get; set; } = ContainerAlignment.Stretch;
    [Parameter] public bool IsEnabled { get; set; } = true;
    [Parameter] public RenderFragment? ChildContent { get; set; }

    public string FinalOuterStyles { get; private set; } = string.Empty;
    public string FinalInnerStyles { get; private set; } = string.Empty;
    public bool IsActuallyEnabled => IsEnabled && IsVisible;

    protected override void OnParametersSet()
    {
        if (!IsVisible)
        {
            FinalOuterStyles = "display: none;";
            return;
        }

        FinalOuterStyles = ContainerStyleBuilder.BuildOuterStyles(
            HorizontalAlignment, VerticalAlignment,
            Width, Height,
            MarginLeft, MarginRight, MarginTop, MarginBottom,
            IsEnabled);

        FinalInnerStyles = ContainerStyleBuilder.BuildInnerStyles(
            PaddingLeft, PaddingRight, PaddingTop, PaddingBottom);

        base.OnParametersSet();
    }
}