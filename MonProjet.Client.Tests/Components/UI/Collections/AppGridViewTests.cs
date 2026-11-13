using Bunit;
using MonProjet.Client.Components.UI.Collections;
using Xunit;

namespace MonProjet.Client.Tests.Components.UI.Collections;

[Trait("Category", "Component")]
public sealed class AppGridViewTests : BunitContext
{
    [Fact]
    public void Render_UsesItemTemplateAndShowsEmptyState()
    {
        var cut = Render<AppGridView<string>>(parameters => parameters
            .Add(component => component.Items, ["Card A", "Card B"])
            .Add(component => component.ItemTemplate, item => builder => builder.AddContent(0, item)));

        Assert.Equal(2, cut.FindAll(".app-gridview-item").Count);
        Assert.Contains("Card A", cut.Markup);

        cut.Render(parameters => parameters
            .Add(component => component.Items, []));

        Assert.Contains("Aucun élément à afficher.", cut.Markup);
    }
}