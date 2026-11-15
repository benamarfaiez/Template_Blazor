using Bunit;
using MonProjet.Client.Components.UI.BasicInput;
using Xunit;

namespace MonProjet.Client.Tests.Components.UI.BasicInput;

[Trait("Category", "Component")]
public sealed class AppNumberBoxTests : BunitContext
{
    [Fact]
    public void Increment_ClampsValueAtMaximum()
    {
        int? changed = null;

        var cut = Render<AppNumberBox<int>>(parameters => parameters
            .Add(component => component.Value, 4)
            .Add(component => component.Step, 2)
            .Add(component => component.Max, 5)
            .Add(component => component.ValueChanged, value => changed = value));

        cut.Find("button[aria-label='Augmenter']").Click();

        Assert.Equal(5, changed);
        Assert.Equal("5", cut.Find("input").GetAttribute("value"));
    }
}