using BlazorApp.Tool.GUI.Components;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Components.UIElements;

public partial class UIGridPresenter : ComponentBase
{
    [Parameter]
    public Tool.GUI.Components.IUIGrid UIGrid { get; set; } = default!;

    protected override void OnInitialized()
    {
        base.OnInitialized();

        UIGrid.CellsChanged += UIGrid_CellsChanged;
    }

    public void Dispose()
    {
        UIGrid.CellsChanged += UIGrid_CellsChanged;
    }

    private void UIGrid_CellsChanged(object? sender, EventArgs e)
    {
        StateHasChanged();
    }
}
