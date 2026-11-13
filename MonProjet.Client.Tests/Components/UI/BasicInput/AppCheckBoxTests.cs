using Bunit;
using MonProjet.Client.Components.UI.BasicInput;
using Xunit;

namespace MonProjet.Client.Tests.Components.UI.BasicInput;

[Trait("Category", "Component")]
public sealed class AppCheckBoxTests : BunitContext
{
    [Fact]
    public void Change_UpdatesValueAndInvokesCallback()
    {
        bool? changedValue = null;

        var cut = Render<AppCheckBox>(parameters => parameters
            .Add(component => component.Value, false)
            .Add(component => component.Label, "Accept")
            .Add(component => component.ValueChanged, value => changedValue = value));

        cut.Find("input[type='checkbox']").Change(true);

        Assert.Equal("Accept", cut.Find("label.app-checkbox").TextContent.Trim());
        Assert.Equal(true, changedValue);
    }
}