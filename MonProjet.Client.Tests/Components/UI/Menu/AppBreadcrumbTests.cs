using Bunit;
using MonProjet.Client.Components.UI.Menu;
using MonProjet.Client.Models.UI.Menu;
using Xunit;

namespace MonProjet.Client.Tests.Components.UI.Menu;

[Trait("Category", "Component")]
public sealed class AppBreadcrumbTests : BunitContext
{
    [Fact]
    public void Render_ExposesCurrentPageAndLinkedAncestors()
    {
        var cut = Render<AppBreadcrumb>(parameters => parameters
            .Add(component => component.Items, [new BreadcrumbItem("Home", "/"), new BreadcrumbItem("Current")]));

        Assert.Equal("breadcrumb", cut.Find("nav").GetAttribute("aria-label"));
        Assert.Equal("/", cut.Find("a").GetAttribute("href"));

        Assert.Equal("page", cut.Find("[aria-current='page']").GetAttribute("aria-current"));
        Assert.Contains("Current", cut.Markup);
    }
}