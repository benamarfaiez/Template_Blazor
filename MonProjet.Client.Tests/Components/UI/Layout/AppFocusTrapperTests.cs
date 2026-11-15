using Bunit;
using MonProjet.Client.Components.UI.Layout;
using Xunit;

namespace MonProjet.Client.Tests.Components.UI.Layout;

[Trait("Category", "Component")]
public sealed class AppFocusTrapperTests : BunitContext
{
    [Fact]
    public void Render_PreservesChildContent()
    {
        var module = JSInterop.SetupModule("./js/focusTrap.js");

        module.SetupVoid("trap", _ => true);
        module.SetupVoid("release", _ => true);

        var cut = Render<AppFocusTrapper>(parameters => parameters.AddChildContent("Focusable content"));

        Assert.Equal("Focusable content", cut.Find(".app-focus-trapper").TextContent);
    }
}