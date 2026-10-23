using BlazorApp.Tool.GUI.Components;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Components.UIElements;

public partial class UIProgressBarPresenter : ComponentBase, IDisposable
{
    [Parameter]
    public Tool.GUI.Components.IUIProgressBar UIProgressBar { get; set; } = default!;

    protected override void OnInitialized()
    {
        base.OnInitialized();

        if (UIProgressBar is not null)
        {
            UIProgressBar.ValueChangingAsynchronously += UIProgressBar_ValueChangingAsynchronously;
        }
    }

    public void Dispose()
    {
        if (UIProgressBar is not null)
        {
            UIProgressBar.ValueChangingAsynchronously -= UIProgressBar_ValueChangingAsynchronously;
        }
        GC.SuppressFinalize(this);
    }

    private void UIProgressBar_ValueChangingAsynchronously(object? sender, double percentage)
    {
        InvokeAsync(() =>
        {
            UIProgressBar?.Progress(percentage);
        });
    }
}
