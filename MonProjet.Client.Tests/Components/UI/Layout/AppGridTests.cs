using Bunit;
using MonProjet.Client.Components.UI.Layout;
using Xunit;

namespace MonProjet.Client.Tests.Components.UI.Layout;

[Trait("Category", "Component")]
public sealed class AppGridTests : BunitContext
{
    [Fact]
    public void Render_ExposesColumnConfigurationAndContent()
    {
        var cut = Render<AppGrid>(parameters => parameters
            .Add(component => component.Columns, 4)
            .Add(component => component.Gap, "12px")
            .AddChildContent("Grid item"));

        Assert.Contains("--grid-columns: 4", cut.Find(".app-grid").GetAttribute("style"));
        Assert.Contains("Grid item", cut.Markup);
    }
}