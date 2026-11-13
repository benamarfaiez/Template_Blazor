using Bunit;
using MonProjet.Client.Components.UI.Enums.Text;
using MonProjet.Client.Components.UI.Text;
using Xunit;

namespace MonProjet.Client.Tests.Components.UI.Text;

[Trait("Category", "Component")]
public sealed class TextBlockTests : BunitContext
{
    [Fact]
    public void Render_UsesRequestedSemanticHeading()
    {
        var cut = Render<TextBlock>(parameters => parameters
            .Add(component => component.Text, "Section title")
            .Add(component => component.As, TextElement.H2));

        Assert.Equal("Section title", cut.Find("h2").TextContent);
    }
}