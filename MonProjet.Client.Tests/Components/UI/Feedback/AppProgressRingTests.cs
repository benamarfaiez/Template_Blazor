using Bunit;
using MonProjet.Client.Components.UI.Feedback;
using Xunit;

namespace MonProjet.Client.Tests.Components.UI.Feedback;

[Trait("Category", "Component")]
public sealed class AppProgressRingTests : BunitContext
{
    [Fact]
    public void Render_AnnouncesLoadingStatus()
    {
        var cut = Render<AppProgressRing>();

        Assert.Equal("status", cut.Find("svg").GetAttribute("role"));
        Assert.Equal("Chargement en cours...", cut.Find("svg").GetAttribute("aria-label"));
    }
}