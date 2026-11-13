using Bunit;
using MonProjet.Client.Components.UI.Menu.NavBar;
using Xunit;

namespace MonProjet.Client.Tests.Components.UI.Menu.NavBar;

[Trait("Category", "Component")]
public sealed class AppNavBarBrandTests : BunitContext
{
    [Fact]
    public void Render_ExposesBrandNameAndAccessibleLink()
    {
        var cut = Render<AppNavBarBrand>(parameters => parameters
            .Add(component => component.Brand, "Acme")
            .Add(component => component.Href, "/home")
            .Add(component => component.AriaLabel, "Acme home"));

        Assert.Equal("/home", cut.Find("a").GetAttribute("href"));
        Assert.Equal("Acme home", cut.Find("a").GetAttribute("aria-label"));
        Assert.Contains("Acme", cut.Find("a").TextContent);
    }
}