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
    [Parameter] public bool ShowPagination { get; set; } = true;
    [Parameter] public int PageSize { get; set; } = 10;

    private IEnumerable<TItem>? _lastItems;
    private List<GridColumn<TItem>>? _lastColumns;

    private readonly GridSortState<TItem> _sortState = new();
    private readonly GridPaginationState _paginationState = new();

    private List<TItem> _sortedItems = [];
    private List<TItem> _pagedItems = [];

    protected override void OnParametersSet()
    {
        _paginationState.PageSize = PageSize;

        if (Items != _lastItems || Columns != _lastColumns)
        {
            UpdateSortedItems();
            _lastItems = Items;
            _lastColumns = Columns;
        }

        base.OnParametersSet();
    }

    private void HandleSortRequested(GridColumn<TItem> column)
    {
        _sortState.ToggleSort(column);
        _paginationState.Reset();
        UpdateSortedItems();
        StateHasChanged();
    }

    private void UpdateSortedItems()
    {
        if (Items is null)
        {
            _sortedItems.Clear();
            _pagedItems.Clear();
            _paginationState.UpdateTotalItems(0);
            return;
        }

        var sorted = _sortState.Apply(Items, Columns);
        _sortedItems = [.. sorted];

        _paginationState.UpdateTotalItems(_sortedItems.Count);
        UpdatePagedItems();
    }

    private void UpdatePagedItems()
    {
        if (!ShowPagination)
        {
            _pagedItems = _sortedItems;
            return;
        }

        _pagedItems = [.. _sortedItems
            .Skip((_paginationState.CurrentPage - 1) * _paginationState.PageSize)
            .Take(_paginationState.PageSize)];
    }

    private void HandlePageChanged(int newPage)
    {
        _paginationState.GoToPage(newPage);
        UpdatePagedItems();
        StateHasChanged();
    }
}