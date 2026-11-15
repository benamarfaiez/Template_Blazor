using Bunit;
using Microsoft.AspNetCore.Components;
using MonProjet.Client.Components.UI.Stepper;
using Xunit;

namespace MonProjet.Client.Tests.Components.UI.Stepper;

[Trait("Category", "Component")]
public sealed class AppStepTests : BunitContext
{
    [Fact]
    public void Step_RegistersTitleAndContentWithParentStepper()
    {
        var cut = Render<AppStepper>(parameters => parameters
            .Add(component => component.ChildContent, BuildSteps()));

        Assert.Contains("Payment", cut.Markup);
        Assert.Contains("Payment details", cut.Markup);
    }

    private static RenderFragment BuildSteps() => builder =>
    {
        builder.OpenComponent<AppStep>(0);
        builder.AddAttribute(1, nameof(AppStep.Title), "Payment");
        builder.AddAttribute(2, nameof(AppStep.ChildContent), (RenderFragment)(content => content.AddContent(0, "Payment details")));
        builder.CloseComponent();
    };
}