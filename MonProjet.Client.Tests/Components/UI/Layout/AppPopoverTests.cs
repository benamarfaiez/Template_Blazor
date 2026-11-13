using Bunit;
using MonProjet.Client.Components.UI.Layout;
using Xunit;

namespace MonProjet.Client.Tests.Components.UI.Layout;

[Trait("Category", "Component")]
public sealed class AppPopoverTests : BunitContext
{
    [Fact]
    public void ClickAnchor_OpensDialogAndUpdatesExpandedState()
    {
        bool? open = null;

        var cut = Render<AppPopover>(parameters => parameters
            .Add(component => component.IsOpen, false)
            .Add(component => component.IsOpenChanged, value => open = value)
            .Add(component => component.Anchor, builder => builder.AddContent(0, "Help"))
            .Add(component => component.Content, builder => builder.AddContent(0, "Help text")));

        cut.Find("[role='button']").Click();

        Assert.Equal(true, open);
        Assert.Equal("true", cut.Find("[role='button']").GetAttribute("aria-expanded"));
        Assert.Contains("Help text", cut.Find("[role='dialog']").TextContent);
    }
}