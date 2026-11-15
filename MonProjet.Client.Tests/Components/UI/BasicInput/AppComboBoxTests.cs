using Bunit;
using MonProjet.Client.Components.UI.BasicInput;
using Xunit;

namespace MonProjet.Client.Tests.Components.UI.BasicInput;

[Trait("Category", "Component")]
public sealed class AppComboBoxTests : BunitContext
{
    [Fact]
    public void Change_EmitsSelectedItemAndRendersPlaceholder()
    {
        string? selected = null;

        var cut = Render<AppComboBox<string>>(parameters => parameters
            .Add(component => component.Items, ["One", "Two"])
            .Add(component => component.Placeholder, "Choose")
            .Add(component => component.SelectedItemChanged, value => selected = value));

        cut.Find("select").Change("Two");

        // Vérifications
        Assert.Equal("Choose", cut.Find("option[value='']").TextContent);
        Assert.Equal("Two", selected);
    }
}