using Microsoft.AspNetCore.Components;
using MonProjet.Client.Components.UI.Core;

namespace MonProjet.Client.Components.UI.Stepper;

public partial class AppStepper : AppComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public int ActiveIndex { get; set; } = 0;
    [Parameter] public EventCallback<int> ActiveIndexChanged { get; set; }
    [Parameter] public StepperOrientation Orientation { get; set; } = StepperOrientation.Horizontal;
    [Parameter] public Func<int, int, Task<bool>>? OnBeforeStepChange { get; set; }
    [Parameter] public bool ShowNavigationButtons { get; set; } = true;
    [Parameter] public EventCallback OnFinished { get; set; }
    [Parameter] public bool IsFinishing { get; set; }

    internal List<StepItem> Steps { get; } = [];

    internal void AddStep(StepItem step)
    {
        if (Steps.Any(s => s.Id == step.Id)) return;
        Steps.Add(step);
        StateHasChanged();
    }

    internal void RemoveStep(StepItem step)
    {
        Steps.Remove(step);
        StateHasChanged();
    }

    private async Task GoToStepAsync(int index)
    {
        if (index < 0 || index >= Steps.Count || index == ActiveIndex) return;
        if (Steps[index].IsDisabled) return;

        if (OnBeforeStepChange != null)
        {
            bool canChange = await OnBeforeStepChange(ActiveIndex, index);
            if (!canChange) return;
        }

        ActiveIndex = index;
        if (ActiveIndexChanged.HasDelegate)
        {
            await ActiveIndexChanged.InvokeAsync(ActiveIndex);
        }
    }

    private async Task NextAsync() => await GoToStepAsync(ActiveIndex + 1);
    private async Task PreviousAsync() => await GoToStepAsync(ActiveIndex - 1);

    private async Task FinishAsync()
    {
        if (OnFinished.HasDelegate)
        {
            await OnFinished.InvokeAsync();
        }
    }

    private string GetStepClass(int index)
    {
        if (index < ActiveIndex) return "completed";
        if (index == ActiveIndex) return "active";
        return "pending";
    }

    public enum StepperOrientation { Horizontal, Vertical }
}