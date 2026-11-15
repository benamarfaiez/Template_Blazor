using Bunit;
using Microsoft.AspNetCore.Components.Web;
using MonProjet.Client.Components.UI.Text;
using Xunit;

namespace MonProjet.Client.Tests.Components.UI.Text;

[Trait("Category", "Component")]
public sealed class AutoSuggestBoxTests : BunitContext
{
    [Fact]
    public void Input_FiltersSuggestionsAndEnterSelectsFirstMatch()
    {
        string? selected = null;

        var cut = Render<AutoSuggestBox<string>>(parameters => parameters
            .Add(component => component.Items, ["Apple", "Apricot", "Banana"])
            .Add(component => component.ValueChanged, value => selected = value));

        var input = cut.Find("input[role='combobox']");

        input.Input("ap");

        Assert.Equal("true", input.GetAttribute("aria-expanded"));
        Assert.Equal(2, cut.FindAll("[role='option']").Count);

        input.KeyDown(new KeyboardEventArgs { Key = "Enter" });

        Assert.Equal("Apple", selected);
        Assert.Equal("false", cut.Find("input[role='combobox']").GetAttribute("aria-expanded"));
    }
}