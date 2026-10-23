using BlazorApp.Tool.GUI.Components;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Components.UIElements;

public partial class UIButtonPresenter : ComponentBase
{
    [Parameter]
    public Tool.GUI.Components.IUIButton UIButton { get; set; } = default!;

    internal async Task OnButtonClickAsync()
    {
        Guard.IsNotNull(UIButton);
        if (UIButton.OnClickAction is not null)
        {
            await UIButton.OnClickAction.Invoke(); // TODO: Try Catch and log?
        }
    }
}
