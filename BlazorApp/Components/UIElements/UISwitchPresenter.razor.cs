using BlazorApp.Tool.GUI.Components;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Components.UIElements;

public partial class UISwitchPresenter : ComponentBase
{
    [Parameter]
    public Tool.GUI.Components.IUISwitch UISwitch { get; set; } = default!;

    private void OnCheckedChanged(bool value)
    {
        if (value)
        {
            UISwitch.On();
        }
        else
        {
            UISwitch.Off();
        }
    }
}
