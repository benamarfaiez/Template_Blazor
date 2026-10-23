using BlazorApp.Tool.GUI.Components;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Components.UIElements;

public partial class UIWrapPresenter : ComponentBase
{
    [Parameter]
    public Tool.GUI.Components.IUIWrap UIWrap { get; set; } = default!;
}
