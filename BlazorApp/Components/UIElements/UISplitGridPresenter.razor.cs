using BlazorApp.Tool.GUI.Components;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Components.UIElements;

public partial class UISplitGridPresenter : ComponentBase
{
    [Parameter]
    public Tool.GUI.Components.IUISplitGrid UISplitGrid { get; set; } = default!;
}
