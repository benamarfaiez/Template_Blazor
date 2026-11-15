using Bunit;
using Microsoft.AspNetCore.Components;
using MonProjet.Client.Components.UI.Stepper;
using Xunit;

namespace MonProjet.Client.Tests.Components.UI.Stepper;

[Trait("Category", "Component")]
public sealed class AppStepperTests : BunitContext
{
    [Fact]
    public void NextButton_AdvancesToNextStepAndUpdatesTabSelection()
    {
        int? activeIndex = null;

        var cut = Render<AppStepper>(parameters => parameters
            .Add(component => component.ActiveIndexChanged, value => activeIndex = value)
            .Add(component => component.ChildContent, BuildSteps()));

        // On cherche le bouton "Suivant" et on clique dessus
        cut.FindAll("button").Single(button => button.TextContent.Trim() == "Suivant").Click();

        // Vérifications
        Assert.Equal(1, activeIndex);

        Assert.Equal("true", cut.FindAll("[role='tab']")[1].GetAttribute("aria-selected"));
        Assert.Contains("Second content", cut.Find(".app-stepper-content").TextContent);
    }

    private static RenderFragment BuildSteps() => builder =>
    {
        builder.OpenComponent<AppStep>(0);
        builder.AddAttribute(1, nameof(AppStep.Title), "First");
        builder.AddAttribute(2, nameof(AppStep.ChildContent), (RenderFragment)(content => content.AddContent(0, "First content")));
        builder.CloseComponent();

        builder.OpenComponent<AppStep>(3);
        builder.AddAttribute(4, nameof(AppStep.Title), "Second");
        builder.AddAttribute(5, nameof(AppStep.ChildContent), (RenderFragment)(content => content.AddContent(0, "Second content")));
        builder.CloseComponent();
    };
}