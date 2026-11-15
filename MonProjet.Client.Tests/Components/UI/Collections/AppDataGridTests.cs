using Bunit;
using MonProjet.Client.Components.UI.Collections;
using MonProjet.Client.Components.UI.Collections.Column;
using Xunit;

namespace MonProjet.Client.Tests.Components.UI.Collections;

[Trait("Category", "Component")]
public sealed class AppDataGridTests : BunitContext
{
    [Fact]
    public void Render_DisplaysRowsAndUsesEmptyTemplateWhenNoItems()
    {
        var columns = new List<GridColumn<string>>
        {
            new("Name", new ValueContent<string>(item => item))
        };

        var cut = Render<AppDataGrid<string>>(parameters => parameters
            .Add(component => component.Items, ["Alpha", "Beta"])
            .Add(component => component.Columns, columns)
            .Add(component => component.ShowPagination, false));

        Assert.Equal(2, cut.FindAll("tbody tr").Count);
        Assert.Contains("Alpha", cut.Markup);

        cut.Render(parameters => parameters
            .Add(component => component.Items, []));

        Assert.Contains("Aucune donnée disponible.", cut.Markup);
    }
}