using Bunit;
using MonProjet.Client.Components.UI.Layout;
using Xunit;

namespace MonProjet.Client.Tests.Components.UI.Layout;

[Trait("Category", "Component")]
public sealed class AppDialogTests : BunitContext
{
    [Fact]
    public void CloseButton_EmitsHiddenStateAndRemovesDialog()
    {
        var module = JSInterop.SetupModule("./js/focusTrap.js");
        module.SetupVoid("trap", _ => true);
        module.SetupVoid("release", _ => true);

        bool? visible = null;

        var cut = Render<AppDialog>(parameters => parameters
            .Add(component => component.IsVisible, true)
            .Add(component => component.Title, "Confirm")
            .Add(component => component.IsVisibleChanged, value => visible = value));

        Assert.Equal("true", cut.Find("[role='dialog']").GetAttribute("aria-modal"));

        cut.Find("button[aria-label='Fermer la fenêtre']").Click();

        Assert.Equal(false, visible);

        // Assert.Contains("display: none", cut.Find(".app-dialog-overlay").GetAttribute("style"));
        Assert.Empty(cut.FindAll("[role='dialog']"));
    }
}