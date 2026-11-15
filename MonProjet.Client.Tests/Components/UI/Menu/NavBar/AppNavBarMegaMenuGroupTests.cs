using Bunit;
using MonProjet.Client.Components.UI.Menu.NavBar;
using MonProjet.Client.Models.UI.Menu.NavBar;
using Xunit;

namespace MonProjet.Client.Tests.Components.UI.Menu.NavBar;

[Trait("Category", "Component")]
public sealed class AppNavBarMegaMenuGroupTests : BunitContext
{
    [Fact]
    public void Render_ShowsGroupTitleAndItemBadge()
    {
        var cut = Render<AppNavBarMegaMenuGroup>(parameters => parameters
            .Add(component => component.Group, new MegaMenuGroup
            {
                Title = "Finance",
                Items = [new MegaMenuItem { Title = "Reports", Badge = "New" }]
            }));

        Assert.Contains("Finance", cut.Find("h4").TextContent);
        Assert.Contains("Reports", cut.Markup);
        Assert.Contains("New", cut.Markup);
    }
}