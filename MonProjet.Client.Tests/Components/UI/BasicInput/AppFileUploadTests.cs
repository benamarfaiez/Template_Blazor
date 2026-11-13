using Bunit;
using MonProjet.Client.Components.UI.BasicInput;
using MonProjet.Client.Models.UI.Files;
using Xunit;

namespace MonProjet.Client.Tests.Components.UI.BasicInput;

[Trait("Category", "Component")]
public sealed class AppFileUploadTests : BunitContext
{
    [Fact]
    public void Render_ShowsUploadTextAndConfiguredFileTypes()
    {
        JSInterop.SetupModule("./js/file-drop.js").SetupVoid("initDropZone");

        var cut = Render<AppFileUpload>(parameters => parameters
            .Add(component => component.Config, new FileUploadConfig { AllowedExtensions = [".pdf"] }));

        Assert.Contains("Glissez-déposez vos fichiers ici", cut.Markup);

        Assert.Equal(".pdf", cut.Find("input[type='file']").GetAttribute("accept"));
    }
}