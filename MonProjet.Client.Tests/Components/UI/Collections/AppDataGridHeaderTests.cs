using Bunit;
using MonProjet.Client.Components.UI.Collections;
using MonProjet.Client.Components.UI.Collections.Column;
using MonProjet.Client.Components.UI.Collections.State;
using Xunit;

namespace MonProjet.Client.Tests.Components.UI.Collections;

[Trait("Category", "Component")]
public sealed class AppDataGridHeaderTests : BunitContext
{
    [Fact]
    public void ClickSortableHeader_EmitsColumnAndExposesSortSemantics()
    {
        var column = new GridColumn<string>(
            "Name",
            new ValueContent<string>(item => item),
            new ComparerSort<string>((left, right) => string.CompareOrdinal(left, right)));

        GridColumn<string>? requested = null;

        var cut = Render<AppDataGridHeader<string>>(parameters => parameters
            .Add(component => component.Columns, [column])
            .Add(component => component.SortState, new GridSortState<string>())
            .Add(component => component.OnSortRequested, value => requested = value));

        // Vérification de l'attribut ARIA initial
        Assert.Equal("none", cut.Find("th").GetAttribute("aria-sort"));

        // Déclenchement du clic
        cut.Find("th").Click();

        // Vérification que l'événement est remonté avec la bonne colonne
        Assert.Same(column, requested);
    }
}