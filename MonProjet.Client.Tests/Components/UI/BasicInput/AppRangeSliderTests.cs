using Bunit;
using MonProjet.Client.Components.UI.BasicInput;
using Xunit;

namespace MonProjet.Client.Tests.Components.UI.BasicInput;

[Trait("Category", "Component")]
public sealed class AppRangeSliderTests : BunitContext
{
    [Fact]
    public void Input_ClampsValueAndUpdatesAriaValue()
    {
        int? changed = null;

        var cut = Render<AppRangeSlider<int>>(parameters => parameters
            .Add(component => component.Value, 50)
            .Add(component => component.Min, 0)
            .Add(component => component.Max, 100)
            .Add(component => component.ValueChanged, value => changed = value));

        cut.Find("input[type='range']").Input("150");

        // Vérifications
        Assert.Equal(100, changed);
        Assert.Equal("100", cut.Find("input[type='range']").GetAttribute("aria-valuenow"));
    }
}