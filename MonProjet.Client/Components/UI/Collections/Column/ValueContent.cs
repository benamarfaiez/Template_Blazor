using Microsoft.AspNetCore.Components;

namespace MonProjet.Client.Components.UI.Collections.Column;

public class ValueContent<TItem>(Func<TItem, object?> valueSelector, Func<object?, string>? formatter = null) : IColumnContent<TItem>
{
    private readonly Func<TItem, object?> _valueSelector = valueSelector;
    private readonly Func<object?, string>? _formatter = formatter;

    public RenderFragment Render(TItem item) => builder =>
    {
        var raw = _valueSelector(item);
        var text = _formatter?.Invoke(raw) ?? raw?.ToString() ?? string.Empty;
        builder.AddContent(0, text);
    };
}

