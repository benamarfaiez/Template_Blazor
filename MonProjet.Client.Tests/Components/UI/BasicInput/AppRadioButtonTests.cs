using Bunit;
using MonProjet.Client.Components.UI.BasicInput;
using Xunit;

namespace MonProjet.Client.Tests.Components.UI.BasicInput;

[Trait("Category", "Component")]
public sealed class AppRadioButtonTests : BunitContext
{
    [Fact]
    public void Change_EmitsValueAndRendersCheckedState()
    {
        string? selected = null;

        var cut = Render<AppRadioButton>(parameters => parameters
            .Add(component => component.Value, "option-a")
            .Add(component => component.CurrentValue, "option-a")
            .Add(component => component.CurrentValueChanged, value => selected = value));

        Assert.True(cut.Find("input[type='radio']").HasAttribute("checked"));

        cut.Find("input[type='radio']").Change("option-a");

        Assert.Equal("option-a", selected);
    }
}