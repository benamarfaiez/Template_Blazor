using Bunit;
using MonProjet.Client.Components.UI.Menu.NavBar;
using MonProjet.Client.Models.UI.Menu.NavBar;
using Xunit;

namespace MonProjet.Client.Tests.Components.UI.Menu.NavBar;

[Trait("Category", "Component")]
public sealed class AppNavBarMegaMenuItemTests : BunitContext
{
    [Fact]
    public void Click_InvokesCallbackAndRendersDestination()
    {
        var clicked = false;

        var cut = Render<AppNavBarMegaMenuItem>(parameters => parameters
            .Add(component => component.Item, new MegaMenuItem { Title = "Dashboard", Href = "/dashboard", Description = "Overview" })
            .Add(component => component.OnClick, () => clicked = true));

        cut.Find("a.mega-item").Click();

        Assert.True(clicked);
        Assert.Equal("/dashboard", cut.Find("a.mega-item").GetAttribute("href"));
        Assert.Contains("Overview", cut.Markup);
    }
}