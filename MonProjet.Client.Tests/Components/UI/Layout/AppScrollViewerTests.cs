using Bunit;
using MonProjet.Client.Components.UI.Enums;
using MonProjet.Client.Components.UI.Layout;
using Xunit;

namespace MonProjet.Client.Tests.Components.UI.Layout;

[Trait("Category", "Component")]
public sealed class AppScrollViewerTests : BunitContext
{
    [Fact]
    public void Render_AppliesMaxHeightAndDirection()
    {
        var cut = Render<AppScrollViewer>(parameters => parameters
            .Add(component => component.MaxHeight, "240px")
            .Add(component => component.Direction, ScrollDirection.Horizontal)
            .AddChildContent("Scrollable content"));

        var viewer = cut.Find(".app-scrollviewer");

        Assert.Contains("--scroll-max-height: 240px", viewer.GetAttribute("style"));
        Assert.Contains("overflow-x: auto", viewer.GetAttribute("style"));
        Assert.Contains("Scrollable content", viewer.TextContent);
    }
}