using Microsoft.AspNetCore.Components;
using MonProjet.Client.Components.UI.Core;

namespace MonProjet.Client.Components.UI.Stepper;

public partial class AppStep : AppComponentBase, IDisposable
{
    [CascadingParameter] private AppStepper? Parent { get; set; }

    [Parameter] public string Title { get; set; } = string.Empty;
    [Parameter] public string? Subtitle { get; set; }
    [Parameter] public string? Icon { get; set; }
    [Parameter] public bool IsDisabled { get; set; }
    [Parameter] public RenderFragment? ChildContent { get; set; }

    private StepItem _stepItem = null!;

    protected override void OnInitialized()
    {
        if (Parent == null)
            throw new InvalidOperationException("AppStep doit être placé à l'intérieur d'un AppStepper.");

        _stepItem = new StepItem
        {
            Title = Title,
            Subtitle = Subtitle,
            Icon = Icon,
            IsDisabled = IsDisabled,
            Content = ChildContent
        };

        Parent.AddStep(_stepItem);
    }

    public void Dispose()
    {
        Parent?.RemoveStep(_stepItem);
        GC.SuppressFinalize(this);
    }
}