using Bunit;
using MonProjet.Client.Components.UI.Layout;
using Xunit;

namespace MonProjet.Client.Tests.Components.UI.Layout;

[Trait("Category", "Component")]
public sealed class AppOverlayTests : BunitContext
{
    [Fact]
    public void OverlayClick_EmitsHiddenState()
    {
        var module = JSInterop.SetupModule("./js/focusTrap.js");
        module.SetupVoid("trap", _ => true);
        module.SetupVoid("release", _ => true);

        bool? visible = null;

        var cut = Render<AppOverlay>(parameters => parameters
            .Add(component => component.IsVisible, true)
            .Add(component => component.TrapFocus, false)
            .Add(component => component.IsVisibleChanged, value => visible = value)
            .AddChildContent("Overlay content"));

        cut.Find(".app-overlay-backdrop").Click();

        Assert.Equal(false, visible);

        // Vérifie que le dialog a bien été retiré du DOM (ou caché)
        Assert.Empty(cut.FindAll("[role='dialog']"));
    }
}