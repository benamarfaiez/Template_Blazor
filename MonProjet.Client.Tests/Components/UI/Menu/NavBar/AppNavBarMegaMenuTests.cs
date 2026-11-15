using Bunit;
using MonProjet.Client.Components.UI.Menu.NavBar;
using MonProjet.Client.Models.UI.Menu.NavBar;
using Xunit;

namespace MonProjet.Client.Tests.Components.UI.Menu.NavBar;

[Trait("Category", "Component")]
public sealed class AppNavBarMegaMenuTests : BunitContext
{
    [Fact]
    public void Render_DisplaysBannerAndGroupLinks()
    {
        var cut = Render<AppNavBarMegaMenu>(parameters => parameters
            .Add(component => component.ActiveLink, new NavLinkItem
            {
                BannerText = "Platform overview",
                BannerLinkText = "Explore",
                BannerLinkUrl = "/platform",
                Groups =
                [
                    new MegaMenuGroup
                    {
                        Title = "Services",
                        Items = [new MegaMenuItem { Title = "Analytics", Href = "/analytics" }]
                    }
                ]
            }));

        Assert.Contains("Platform overview", cut.Markup);
        Assert.Contains("Services", cut.Markup);
        Assert.Contains("Analytics", cut.Markup);
        Assert.Equal("/analytics", cut.Find("a.mega-item").GetAttribute("href"));
    }
}