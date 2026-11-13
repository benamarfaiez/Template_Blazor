using Bunit;
using Microsoft.Extensions.DependencyInjection;
using MonProjet.Client.Components.UI.Feedback;
using MonProjet.Client.Services;
using NSubstitute;
using Xunit;

namespace MonProjet.Client.Tests.Components.UI.Feedback;

[Trait("Category", "Component")]
public sealed class ThemeToggleTests : BunitContext 
{
    [Fact]
    public void Click_RequestsThemeToggle()
    {
        using var context = new BunitContext();

        var themeService = Substitute.For<IThemeService>();
        themeService.IsDarkMode.Returns(false);
        themeService.InitializeAsync().Returns(Task.CompletedTask);
        themeService.ToggleThemeAsync().Returns(Task.CompletedTask);

        context.Services.AddSingleton(themeService);

        var cut = context.Render<ThemeToggle>();

        Assert.Contains("🌙", cut.Markup);

        cut.Find("button[aria-label='Changer de thème']").Click();

        themeService.Received(1).ToggleThemeAsync();
    }
}