using Bunit;
using MonProjet.Client.Components.UI.Layout;
using Xunit;

namespace MonProjet.Client.Tests.Components.UI.Layout;

[Trait("Category", "Component")]
public sealed class AppContainerTests : BunitContext
{
    [Fact]
    public void InvisibleContainer_HidesChildContent()
    {
        var cut = Render<AppContainer>(parameters => parameters
            .Add(component => component.IsVisible, false)
            .AddChildContent("Hidden content"));

        Assert.Contains("display: none", cut.Find(".app-container").GetAttribute("style"));
        Assert.Contains("Hidden content", cut.Markup);
    }
}