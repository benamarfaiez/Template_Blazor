using Bunit;
using MonProjet.Client.Components.UI.Menu.NavBar;
using Xunit;

namespace MonProjet.Client.Tests.Components.UI.Menu.NavBar;

[Trait("Category", "Component")]
public sealed class AppNavBarTests : BunitContext
{
    [Fact]
    public void HamburgerClick_UpdatesExpandedState()
    {
        var cut = Render<AppNavBar>();

        var menuButton = cut.Find("button[aria-label='Menu']");

        // ✅ Maintenant "false" est explicitement rendu
        Assert.Equal("false", menuButton.GetAttribute("aria-expanded"));

        menuButton.Click();

        Assert.Equal("true", cut.Find("button[aria-label='Menu']").GetAttribute("aria-expanded"));
        Assert.Contains("is-open", cut.Find("#app-navbar-links").ClassList);
    }
}