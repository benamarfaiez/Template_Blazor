using Bunit;
using MonProjet.Client.Components.UI.Collections;
using Xunit;

namespace MonProjet.Client.Tests.Components.UI.Collections;

[Trait("Category", "Component")]
public sealed class AppDataGridPaginationTests : BunitContext
{
    [Fact]
    public void NextButton_EmitsNextPageAndShowsRange()
    {
        int? page = null;

        var cut = Render<AppDataGridPagination>(parameters => parameters
            .Add(component => component.CurrentPage, 1)
            .Add(component => component.TotalPages, 2)
            .Add(component => component.TotalItems, 15)
            .Add(component => component.PageSize, 10)
            .Add(component => component.OnPageChanged, value => page = value));

        // Déclenchement du clic
        cut.Find("button[title='Page suivante']").Click();

        // Vérifications
        Assert.Equal(2, page);

        var paginationInfo = cut.Find(".pagination-info");
        Assert.Contains("Affichage de 1 à 10 sur 15 éléments", paginationInfo.TextContent);
    }
}