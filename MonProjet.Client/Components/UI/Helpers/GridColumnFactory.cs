using MonProjet.Client.Components.UI.Collections.Column;

namespace MonProjet.Client.Components.UI.Helpers;

public static class GridColumn
{
    public static GridColumn<T> Text<T>(
        string header,
        Func<T, object?> valueSelector,
        Comparison<T>? comparer = null,
        Func<object?, string>? formatter = null)
    {
        return new GridColumn<T>(header, new ValueContent<T>(valueSelector, formatter))
        {
            Sort = comparer != null ? new ComparerSort<T>(comparer) : new NoSort<T>()
        };
    }

    public static GridColumn<T> Template<T>(
        string header,
        Microsoft.AspNetCore.Components.RenderFragment<T> template,
        Comparison<T>? comparer = null)
    {
        return new GridColumn<T>(header, new TemplateContent<T>(template))
        {
            Sort = comparer != null ? new ComparerSort<T>(comparer) : new NoSort<T>()
        };
    }
}
