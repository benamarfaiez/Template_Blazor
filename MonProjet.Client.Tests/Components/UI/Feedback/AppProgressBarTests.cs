using Bunit;
using MonProjet.Client.Components.UI.Feedback;
using Xunit;

namespace MonProjet.Client.Tests.Components.UI.Feedback;

[Trait("Category", "Component")]
public sealed class AppProgressBarTests : BunitContext
{
    [Fact]
    public void Render_ClampsAccessibleProgressValue()
    {
        var cut = Render<AppProgressBar>(parameters => parameters
            .Add(component => component.Value, 140));

        Assert.Equal("100", cut.Find("[role='progressbar']").GetAttribute("aria-valuenow"));

        var style = cut.Find(".app-progressbar-fill").GetAttribute("style");
        Assert.Contains("width: 100%", style);
    }
}