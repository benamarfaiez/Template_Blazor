using Bunit;
using MonProjet.Client.Components.UI.Layout;
using Xunit;

namespace MonProjet.Client.Tests.Components.UI.Layout;

[Trait("Category", "Component")]
public sealed class AppSplitGridTests : BunitContext
{
    [Fact]
    public void Render_DisplaysBothRegions()
    {
        var cut = Render<AppSplitGrid>(parameters => parameters
            .Add(component => component.Left, builder => builder.AddContent(0, "Left pane"))
            .Add(component => component.Right, builder => builder.AddContent(0, "Right pane")));

        Assert.Contains("Left pane", cut.Find(".app-splitgrid-left").TextContent);
        Assert.Contains("Right pane", cut.Find(".app-splitgrid-right").TextContent);
    }
}