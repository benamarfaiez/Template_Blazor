using Bunit;
using MonProjet.Client.Components.UI.BasicInput;
using Xunit;

namespace MonProjet.Client.Tests.Components.UI.BasicInput;

[Trait("Category", "Component")]
public sealed class AppMultiSelectTests : BunitContext
{
    [Fact]
    public void SelectItem_EmitsSelectionAndDisplaysSelectedTag()
    {
        IEnumerable<string>? selected = null;

        var cut = Render<AppMultiSelect<string>>(parameters => parameters
            .Add(component => component.Items, ["Alpha", "Beta"])
            .Add(component => component.SelectedItems, [])
            .Add(component => component.SelectedItemsChanged, items => selected = items));

        cut.Find(".app-multiselect-control").Click();

        // On cherche l'élément contenant "Beta" et on clique dessus
        cut.FindAll(".app-multiselect-item").Single(item => item.TextContent.Contains("Beta")).Click();

        // Vérifications
        Assert.Contains("Beta", selected!);
        Assert.Contains("Beta", cut.Find(".app-multiselect-tag").TextContent);
    }
}