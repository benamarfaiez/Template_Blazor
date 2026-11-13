using Bunit;
using MonProjet.Client.Components.UI.Menu;
using Xunit;

namespace MonProjet.Client.Tests.Components.UI.Menu;

[Trait("Category", "Component")]
public sealed class AppFooterTests : BunitContext
{
    [Fact]
    public void Render_ShowsConfiguredBrandAndYear()
    {
        var cut = Render<AppFooter>(parameters => parameters
            .Add(component => component.BrandName, "Example")
            .Add(component => component.Year, 2030));

        Assert.Contains("© 2030 Example", cut.Find("footer").TextContent);
    }
}