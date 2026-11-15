using Bunit;
using MonProjet.Client.Components.UI.Layout;
using Xunit;

namespace MonProjet.Client.Tests.Components.UI.Layout;

[Trait("Category", "Component")]
public sealed class AppCardTests : BunitContext
{
    [Fact]
    public void Render_DisplaysChildContent()
    {
        var cut = Render<AppCard>(parameters => parameters.AddChildContent("Card details"));

        Assert.Equal("Card details", cut.Find(".app-card").TextContent.Trim());
    }
}