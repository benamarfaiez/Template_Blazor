namespace MonProjet.Client.Components.UI.Collections.Column;

public interface IColumnSort<TItem>
{
    bool IsSortable { get; }
    IOrderedEnumerable<TItem> Sort(IEnumerable<TItem> items, bool ascending);
}