using Bunit;
using MonProjet.Client.Components.UI.Enums;
using MonProjet.Client.Components.UI.Feedback;
using Xunit;

namespace MonProjet.Client.Tests.Components.UI.Feedback;

[Trait("Category", "Component")]
public sealed class InfoBarTests : BunitContext
{
    [Fact]
    public void Dismiss_InvokesCallbackAndRemovesAlert()
    {
        var dismissed = false;

        var cut = Render<InfoBar>(parameters => parameters
            .Add(component => component.Message, "Connection lost")
            .Add(component => component.Severity, InfoSeverity.Warning)
            .Add(component => component.OnDismiss, () => dismissed = true));

        // Déclenchement du clic
        cut.Find("[role='alert'] button").Click();

        Assert.True(dismissed);

        Assert.False(cut.Instance.IsVisible);

        // Alternative : On vérifie que le conteneur parent est bien caché
        var container = cut.Find(".app-container");
        Assert.Contains("display: none", container.GetAttribute("style"));
    }
}