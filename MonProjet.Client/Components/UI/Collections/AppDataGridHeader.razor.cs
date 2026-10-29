using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MonProjet.Client.Components.UI.Core;
using MonProjet.Client.Components.UI.Collections.Column;
using MonProjet.Client.Components.UI.Collections.State;

namespace MonProjet.Client.Components.UI.Collections;

public partial class AppDataGridHeader<TItem> : AppComponentBase
{
    [Parameter] public List<GridColumn<TItem>> Columns { get; set; } = [];
    [Parameter] public GridSortState<TItem> SortState { get; set; } = default!;
    [Parameter] public EventCallback<GridColumn<TItem>> OnSortRequested { get; set; }

    private async Task HandleSortClickAsync(GridColumn<TItem> column)
    {
        if (column.Sort.IsSortable)
        {
            await OnSortRequested.InvokeAsync(column);
        }
    }

    private async Task HandleKeyDownAsync(KeyboardEventArgs e, GridColumn<TItem> column)
    {
        if (!column.Sort.IsSortable) return;

        if (e.Key == "Enter" || e.Key == " " || e.Code == "Space")
        {
            await OnSortRequested.InvokeAsync(column);
        }
    }

    private string GetAriaSort(GridColumn<TItem> column)
    {
        if (!column.Sort.IsSortable) return "none";
        if (SortState.SortColumnKey != column.SortKey) return "none";

        return SortState.SortAscending ? "ascending" : "descending";
    }

    private string GetSortIconClass(GridColumn<TItem> column)
    {
        if (SortState.SortColumnKey != column.SortKey) return "sort-icon neutral";
        return SortState.SortAscending ? "sort-icon asc" : "sort-icon desc";
    }
}