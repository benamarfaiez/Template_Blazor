using Bunit;
using MonProjet.Client.Components.UI.BasicInput;
using Xunit;

namespace MonProjet.Client.Tests.Components.UI.BasicInput;

[Trait("Category", "Component")]
public sealed class AppDropDownButtonTests : BunitContext
{
    [Fact]
    public void Click_TogglesMenuAndRaisesOpenCallback()
    {
        // ✅ CORRECTION : Ajout de `_ => true` pour accepter n'importe quels arguments
        JSInterop.SetupModule("./js/dropdown.js")
                 .SetupVoid("registerFocusOut", _ => true);

        bool? isOpen = null;

        var cut = Render<AppDropDownButton>(parameters => parameters
            .Add(component => component.Text, "Options")
            .Add(component => component.IsOpen, false)
            .Add(component => component.IsOpenChanged, value => isOpen = value)
            .AddChildContent("<span>Menu item</span>"));

        // Déclenchement du clic sur le bouton
        cut.Find("button").Click();

        Assert.Equal(true, isOpen);
        Assert.Contains("Menu item", cut.Markup);
    }
}