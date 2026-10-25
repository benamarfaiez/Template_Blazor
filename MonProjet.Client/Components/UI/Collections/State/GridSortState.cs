using MonProjet.Client.Components.UI.Collections.Column;

namespace MonProjet.Client.Components.UI.Collections.State;

public class GridSortState<TItem>
{
    public string? SortColumnKey { get; private set; }
    public bool SortAscending { get; private set; } = true;

    public void ToggleSort(GridColumn<TItem> column)
    {
        if (!column.Sort.IsSortable) return;

        if (SortColumnKey == column.SortKey)
        {
            SortAscending = !SortAscending;
        }
        else
        {
            SortColumnKey = column.SortKey;
            SortAscending = true;
        }
    }

    public IEnumerable<TItem> Apply(IEnumerable<TItem> items, List<GridColumn<TItem>> columns)
    {
        if (items == null || SortColumnKey == null) return items ?? [];

        var column = columns.FirstOrDefault(c => c.SortKey == SortColumnKey);
        if (column?.Sort == null || !column.Sort.IsSortable) return items;

        return column.Sort.Sort(items, SortAscending);
    }
}
