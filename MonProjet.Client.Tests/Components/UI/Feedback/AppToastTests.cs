using Bunit;
using Microsoft.Extensions.DependencyInjection;
using MonProjet.Client.Components.UI.Feedback;
using MonProjet.Client.Models.UI.Feedback;
using MonProjet.Client.Services;
using Xunit;

namespace MonProjet.Client.Tests.Components.UI.Feedback;

[Trait("Category", "Component")]
public sealed class AppToastTests : BunitContext
{
    [Fact]
    public void ServiceNotification_RendersToastAndCloseRemovesIt()
    {
        var toastService = new ToastService();
        Services.AddSingleton<IToastService>(toastService);

        var cut = Render<AppToast>();

        // Déclenche l'événement via le service
        toastService.Show(new ToastMessage { Message = "Saved", Title = "Success", DurationMs = 30000 });

        // Vérifie l'affichage asynchrone
        cut.WaitForAssertion(() => Assert.Contains("Saved", cut.Markup));
        Assert.Contains("Success", cut.Markup);

        cut.Find("button[aria-label='Fermer']").Click();

        cut.WaitForAssertion(() => Assert.DoesNotContain("Saved", cut.Markup));
    }
}