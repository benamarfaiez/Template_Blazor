namespace MonProjet.Client.Components.UI.Collections.Column;

public class NoSort<TItem> : IColumnSort<TItem>
{
    public bool IsSortable => false;

    public IOrderedEnumerable<TItem> Sort(IEnumerable<TItem> items, bool ascending)
        => items.OrderBy(_ => 0); // Ne devrait jamais être appelé
}
