using Bunit;
using MonProjet.Client.Components.UI.Layout;
using Xunit;

namespace MonProjet.Client.Tests.Components.UI.Layout;

[Trait("Category", "Component")]
public sealed class AppStackPanelTests : BunitContext
{
    [Fact]
    public void Render_DisplaysChildContent()
    {
        var cut = Render<AppStackPanel>(parameters => parameters.AddChildContent("Stack item"));

        Assert.Contains("Stack item", cut.Find(".app-stackpanel").TextContent);
    }
}