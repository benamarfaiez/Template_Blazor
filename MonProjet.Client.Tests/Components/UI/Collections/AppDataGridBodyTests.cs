using Bunit;
using MonProjet.Client.Components.UI.Collections;
using MonProjet.Client.Components.UI.Collections.Column;
using Xunit;

namespace MonProjet.Client.Tests.Components.UI.Collections;

[Trait("Category", "Component")]
public sealed class AppDataGridBodyTests : BunitContext
{
    [Fact]
    public void ClickRow_EmitsSelectedItemAndAccessibleRole()
    {
        string? selected = null;

        var cut = Render<AppDataGridBody<string>>(parameters => parameters
            .Add(component => component.Items, ["Alpha"])
            .Add(component => component.Columns, [new GridColumn<string>("Name", new ValueContent<string>(item => item))])
            .Add(component => component.OnRowClick, item => selected = item));

        // Déclenchement du clic sur la ligne
        cut.Find("tbody tr").Click();

        // Vérifications
        Assert.Equal("Alpha", selected);
        Assert.Equal("button", cut.Find("tbody tr").GetAttribute("role"));
    }
}