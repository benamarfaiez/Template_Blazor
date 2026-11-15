using Bunit;
using MonProjet.Client.Components.UI.Collections;
using Xunit;

namespace MonProjet.Client.Tests.Components.UI.Collections;

[Trait("Category", "Component")]
public sealed class AppListBoxTests : BunitContext
{
    [Fact]
    public void SelectItem_EmitsValueAndMarksSelectedOption()
    {
        string? selected = null;

        var cut = Render<AppListBox<string>>(parameters => parameters
            .Add(component => component.Items, ["Alpha", "Beta"])
            .Add(component => component.SelectedItemChanged, item => selected = item));

        cut.FindAll("[role='option']")[cut.FindAll("[role='option']").Count - 1].Click();

        Assert.Equal("Beta", selected);
        Assert.Equal("true", cut.FindAll("[role='option']")[cut.FindAll("[role='option']").Count - 1].GetAttribute("aria-selected"));
    }
}