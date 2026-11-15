using MonProjet.Client.Services;
using NSubstitute;
using Xunit;

namespace MonProjet.Client.Tests.Services;

[Trait("Category", "Unit")]
public sealed class LanguageServiceTests
{
    [Fact]
    public async Task ChangeLanguageAsync_UpdatesCulturePersistsAndRaisesEvent()
    {
        var preferenceStore = Substitute.For<ILanguagePreferenceStore>();
        var service = new LanguageService(preferenceStore);
        var changed = 0;
        service.OnLanguageChanged += () => changed++;

        await service.ChangeLanguageAsync("en-US");

        Assert.Equal("en-US", service.CurrentCulture);
        Assert.Equal(1, changed);
        await preferenceStore.Received(1).SaveCultureAsync("en-US");
    }

    [Fact]
    public async Task ChangeLanguageAsync_WhenCultureIsUnchangedDoesNotRaiseEvent()
    {
        var preferenceStore = Substitute.For<ILanguagePreferenceStore>();
        var service = new LanguageService(preferenceStore);
        var changed = 0;
        service.OnLanguageChanged += () => changed++;

        // Le service est initialisé par défaut à "fr-FR"
        await service.ChangeLanguageAsync("fr-FR");

        Assert.Equal(0, changed);
        await preferenceStore.DidNotReceive().SaveCultureAsync(Arg.Any<string>());
    }
}