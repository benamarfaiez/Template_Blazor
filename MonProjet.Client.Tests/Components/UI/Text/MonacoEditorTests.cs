using Bunit;
using MonProjet.Client.Components.UI.Text;
using Xunit;

namespace MonProjet.Client.Tests.Components.UI.Text;

[Trait("Category", "Component")]
public sealed class MonacoEditorTests : BunitContext
{
    [Fact]
    public void Render_ExposesConfiguredEditorHeight()
    {
        JSInterop.SetupModule("./Components/UI/Text/MonacoEditor.razor.js")
                 .SetupVoid("init", _ => true);

        var cut = Render<MonacoEditor>(parameters => parameters
            .Add(component => component.EditorHeight, "320px"));

        Assert.Contains("height: 320px", cut.Find(".app-monaco-wrapper").GetAttribute("style"));
        Assert.NotNull(cut.Find(".app-monaco-editor"));
    }
}