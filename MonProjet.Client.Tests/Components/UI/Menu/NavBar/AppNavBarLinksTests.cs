using Bunit;
using MonProjet.Client.Components.UI.Menu.NavBar;
using MonProjet.Client.Models.UI.Menu.NavBar;
using Xunit;

namespace MonProjet.Client.Tests.Components.UI.Menu.NavBar;

[Trait("Category", "Component")]
public sealed class AppNavBarLinksTests : BunitContext
{
    [Fact]
    public void DropdownLink_ReportsStateWithAriaExpanded()
    {
        NavLinkItem? requested = null;
        var link = new NavLinkItem
        {
            Id = "products",
            Label = "Products",
            Groups = [new MegaMenuGroup { Title = "Group" }]
        };

        var cut = Render<AppNavBarLinks>(parameters => parameters
            .Add(component => component.Links, [link])
            .Add(component => component.OnDropdownToggle, item => requested = item));

        cut.Find("button[aria-haspopup='true']").Click();

        Assert.Same(link, requested);
        Assert.Equal("false", cut.Find("button[aria-haspopup='true']").GetAttribute("aria-expanded"));
    }
}