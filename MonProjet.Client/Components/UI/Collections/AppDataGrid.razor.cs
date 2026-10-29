using Microsoft.AspNetCore.Components;
using MonProjet.Client.Components.UI.Core;
using MonProjet.Client.Components.UI.Collections.Column;
using MonProjet.Client.Components.UI.Collections.State;

namespace MonProjet.Client.Components.UI.Collections;

public partial class AppDataGrid<TItem> : AppComponentBase
{
    [Parameter] public IEnumerable<TItem>? Items { get; set; }
    [Parameter] public List<GridColumn<TItem>> Columns { get; set; } = [];
    [Parameter] public RenderFragment? EmptyTemplate { get; set; }
    [Parameter] public Func<TItem, string>? RowClassSelector { get; set; }
    [Parameter] public EventCallback<TItem> OnRowClick { get; set; }
    private IEnumerable<TItem>? _lastItems;
    private int _lastColumnsHash;

    protected override void OnParametersSet()
    {
        int currentColumnsHash = Columns.GetHashCode();

        if (Items != _lastItems || currentColumnsHash != _lastColumnsHash)
        {
            UpdateSortedItems();
            _lastItems = Items;
            _lastColumnsHash = currentColumnsHash;
        }
        base.OnParametersSet();
    }
    private readonly GridSortState<TItem> _sortState = new();
    private List<TItem> _sortedItems = [];
    private void HandleSortRequested(GridColumn<TItem> column)
    {
        _sortState.ToggleSort(column);
        UpdateSortedItems();
        StateHasChanged();
    }

    private void UpdateSortedItems()
    {
        if (Items is null)
        {
            _sortedItems.Clear();
            return;
        }

        var sorted = _sortState.Apply(Items, Columns);
        _sortedItems = [.. sorted];
    }
}