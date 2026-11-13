using Bunit;
using MonProjet.Client.Components.UI.Feedback;
using Xunit;

namespace MonProjet.Client.Tests.Components.UI.Feedback;

[Trait("Category", "Component")]
public sealed class FontIconTests : BunitContext
{
    [Fact]
    public void NonDecorativeIcon_UsesImageSemanticsAndAccessibleName()
    {
        var cut = Render<FontIcon>(parameters => parameters
            .Add(component => component.Icon, "★")
            .Add(component => component.IsDecorative, false)
            .AddUnmatched("aria-label", "Favorite"));

        var span = cut.Find(".app-fonticon");

        Assert.Equal("img", span.GetAttribute("role"));
        Assert.Equal("false", span.GetAttribute("aria-hidden"));
        Assert.Equal("Favorite", span.GetAttribute("aria-label"));
    }
}