using BlazorApp.Tool.GUI.Components;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Components.UIElements;

public partial class UICardPresenter : ComponentBase
{
    [Parameter]
    public Tool.GUI.Components.IUICard UICard { get; set; } = default!;
}
