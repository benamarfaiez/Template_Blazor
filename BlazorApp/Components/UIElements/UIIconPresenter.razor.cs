using BlazorApp.Tool.GUI.Components;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Components.UIElements;

public partial class UIIconPresenter : ComponentBase
{
    [Parameter]
    public Tool.GUI.Components.IUIIcon UIIcon { get; set; } = default!;
}
