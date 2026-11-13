using Bunit;
using MonProjet.Client.Components.UI.BasicInput;
using Xunit;

namespace MonProjet.Client.Tests.Components.UI.BasicInput;

[Trait("Category", "Component")]
public sealed class AppButtonTests : BunitContext
{
    [Fact]
    public void Click_InvokesCallbackAndDisabledButtonDoesNot()
    {
        var clicks = 0;

        var cut = Render<AppButton>(parameters => parameters
            .Add(component => component.Text, "Save")
            .Add(component => component.OnClick, () => clicks++));

        // Action : Clic sur le bouton actif
        cut.Find("button").Click();

        // Vérifications
        Assert.Equal(1, clicks);
        Assert.Equal("Save", cut.Find("button").TextContent.Trim());

        cut.Render(parameters => parameters.Add(component => component.Disabled, true));

        // Vérifications après désactivation
        Assert.True(cut.Find("button").HasAttribute("disabled"));

        // Action : Clic sur le bouton désactivé (ne doit rien faire)
        cut.Find("button").Click();

        // Le compteur ne doit pas avoir augmenté
        Assert.Equal(1, clicks);
    }
}