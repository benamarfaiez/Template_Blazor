using System.ComponentModel;

namespace BlazorApp.Components.Layout.Popover;

public enum Origin
{
    [Description("top-left")]
    TopLeft,

    [Description("top-center")]
    TopCenter,

    [Description("top-right")]
    TopRight,

    [Description("center-left")]
    CenterLeft,

    [Description("center-center")]
    CenterCenter,

    [Description("center-right")]
    CenterRight,

    [Description("bottom-left")]
    BottomLeft,

    [Description("bottom-center")]
    BottomCenter,

    [Description("bottom-right")]
    BottomRight,
}
