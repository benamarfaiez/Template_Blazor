namespace MonProjet.Client.Components.UI.Collections.Column;

public class ComparerSort<TItem>(Comparison<TItem> comparer) : IColumnSort<TItem>
{
    private readonly Comparison<TItem> _comparer = comparer;

    public bool IsSortable => true;

    public IOrderedEnumerable<TItem> Sort(IEnumerable<TItem> items, bool ascending)
    {
        var comparer = Comparer<TItem>.Create(_comparer);
        return ascending
            ? items.OrderBy(i => i, comparer)
            : items.OrderByDescending(i => i, comparer);
    }
}