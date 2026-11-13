using Bunit;
using MonProjet.Client.Components.UI.Text;
using Xunit;

namespace MonProjet.Client.Tests.Components.UI.Text;

[Trait("Category", "Component")]
public sealed class AppTextBoxTests : BunitContext
{
    [Fact]
    public void Input_EmitsValueAndRendersValidationForScreenReaders()
    {
        string? changedValue = null;

        var cut = Render<AppTextBox>(parameters => parameters
            .Add(component => component.Value, "invalid")
            .Add(component => component.ErrorMessage, "Invalid value")
            .Add(component => component.ForceShowError, true)
            .Add(component => component.ValueChanged, value => changedValue = value));

        cut.Find("input").Input("updated");

        Assert.Equal("updated", changedValue);
        Assert.Equal("true", cut.Find("input").GetAttribute("aria-invalid"));

        Assert.Equal("Invalid value", cut.Find("[role='alert']").TextContent);
    }
}