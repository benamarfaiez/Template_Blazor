using Bunit;
using MonProjet.Client.Components.UI.BasicInput;
using Xunit;

namespace MonProjet.Client.Tests.Components.UI.BasicInput;

[Trait("Category", "Component")]
public sealed class AppToggleSwitchTests : BunitContext
{
    [Fact]
    public void Change_UpdatesSwitchAndAriaState()
    {
        bool? changed = null;

        var cut = Render<AppToggleSwitch>(parameters => parameters
            .Add(component => component.Label, "Enabled")
            .Add(component => component.ValueChanged, value => changed = value));

        cut.Find("input[role='switch']").Change(true);

        Assert.Equal(true, changed);
        Assert.Equal("true", cut.Find("input[role='switch']").GetAttribute("aria-checked"));
    }
}