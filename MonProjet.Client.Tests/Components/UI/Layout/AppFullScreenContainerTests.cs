using Bunit;
using MonProjet.Client.Components.UI.Layout;
using Xunit;

namespace MonProjet.Client.Tests.Components.UI.Layout;

[Trait("Category", "Component")]
public sealed class AppFullScreenContainerTests : BunitContext
{
    [Fact]
    public void CloseButton_EmitsVisibilityChange()
    {
        bool? visible = null;

        var cut = Render<AppFullScreenContainer>(parameters => parameters
            .Add(component => component.IsVisible, true)
            .Add(component => component.Title, "Preview")
            .Add(component => component.IsVisibleChanged, value => visible = value));

        Assert.Equal("true", cut.Find("[role='dialog']").GetAttribute("aria-modal"));

        cut.Find("button[aria-label='Fermer le mode plein écran']").Click();

        Assert.Equal(false, visible);
        Assert.Empty(cut.FindAll("[role='dialog']"));
    }
}