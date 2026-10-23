using Microsoft.AspNetCore.Components;

namespace BlazorApp.Components.UIElements;

public partial class UIListPresenter : ComponentBase, IDisposable
{
    private int _selectedIndex = -1;

    [Parameter]
    public Tool.GUI.Components.IUIList UIList { get; set; } = default!;

    [Parameter]
    public int SelectedIndex { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        UIList.SelectedItemChanged += UIList_SelectedItemChanged;
        UIList_SelectedItemChanged(this, EventArgs.Empty);
    }

    private void UIList_SelectedItemChanged(object? sender, EventArgs e)
    {
        if (UIList.Items is not null && UIList.SelectedItem is not null && UIList.CanSelectItem)
        {
            SelectedIndex = UIList.Items.IndexOf(UIList.SelectedItem);
        }
        else
        {
            SelectedIndex = -1;
            UIList.Select(-1);
        }
    }

    public void Dispose()
    {
        UIList.SelectedItemChanged -= UIList_SelectedItemChanged;
        GC.SuppressFinalize(this);
    }
}
