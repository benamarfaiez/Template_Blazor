using BlazorApp.Tool.GUI.Components;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Components.UIElements;

public partial class UIStackPresenter : ComponentBase
{
    [Parameter]
    public Tool.GUI.Components.IUIStack UIStack { get; set; } = default!;
}
