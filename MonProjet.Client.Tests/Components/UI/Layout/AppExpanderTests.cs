using Bunit;
using MonProjet.Client.Components.UI.Layout;
using Xunit;

namespace MonProjet.Client.Tests.Components.UI.Layout;

[Trait("Category", "Component")]
public sealed class AppExpanderTests : BunitContext
{
    [Fact]
    public void Click_TogglesExpandedContentAndAriaState()
    {
        var cut = Render<AppExpander>(parameters => parameters
            .Add(component => component.Header, "Details")
            .AddChildContent("More information"));

        Assert.Equal("false", cut.Find("button").GetAttribute("aria-expanded"));

        // Déclenchement du clic
        cut.Find("button").Click();

        Assert.Equal("true", cut.Find("button").GetAttribute("aria-expanded"));
        Assert.Contains("More information", cut.Markup);
    }
}