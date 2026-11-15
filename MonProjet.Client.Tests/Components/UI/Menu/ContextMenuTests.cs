using Bunit;
using MonProjet.Client.Components.UI.Menu;
using Xunit;

namespace MonProjet.Client.Tests.Components.UI.Menu;

[Trait("Category", "Component")]
public sealed class ContextMenuTests : BunitContext
{
    [Fact]
    public void Render_PreservesTargetContentUntilContextMenuIsOpened()
    {
        var cut = Render<ContextMenu>(parameters => parameters.AddChildContent("Right-click target"));

        Assert.Contains("Right-click target", cut.Find(".app-context-menu-wrapper").TextContent);

        Assert.Empty(cut.FindAll("[role='menu']"));
    }
}