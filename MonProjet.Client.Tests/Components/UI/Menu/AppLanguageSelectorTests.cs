using Bunit;
using Microsoft.Extensions.DependencyInjection;
using MonProjet.Client.Components.UI.Menu;
using MonProjet.Client.Services;
using NSubstitute;
using Xunit;

namespace MonProjet.Client.Tests.Components.UI.Menu;

[Trait("Category", "Component")]
public sealed class AppLanguageSelectorTests : BunitContext
{
    [Fact]
    public void Render_ShowsCurrentLanguageAndAccessibleDropdownButton()
    {
        JSInterop.SetupModule("./js/dropdown.js").SetupVoid("registerFocusOut", _ => true);

        var languageService = Substitute.For<ILanguageService>();
        languageService.CurrentCulture.Returns("fr-FR");
        Services.AddSingleton(languageService);

        var cut = Render<AppLanguageSelector>();

        Assert.Contains("🇫🇷 FR", cut.Markup);
        Assert.NotNull(cut.Find("button"));
    }
}