using Microsoft.AspNetCore.Components;
using MonProjet.Client.Components.UI.Core;

namespace MonProjet.Client.Components.UI.BasicInput;

public partial class AppComboBox<TItem> : AppComponentBase
{
    [Parameter] public IEnumerable<TItem> Items { get; set; } = [];
    [Parameter] public TItem? SelectedItem { get; set; }
    [Parameter] public EventCallback<TItem?> SelectedItemChanged { get; set; }

    [Parameter] public Func<TItem, string> ValueSelector { get; set; } = i => i?.ToString() ?? "";
    [Parameter] public Func<TItem, string> LabelSelector { get; set; } = i => i?.ToString() ?? "";

    [Parameter] public string Placeholder { get; set; } = string.Empty;
    [Parameter] public bool Disabled { get; set; }

    private string GetValue(TItem? item) => item is null ? string.Empty : ValueSelector(item);
    private string GetLabel(TItem item) => LabelSelector(item);

    private Task OnChanged(ChangeEventArgs e)
    {
        var val = e.Value?.ToString();
        var match = Items.FirstOrDefault(i => ValueSelector(i) == val);
        return SelectedItemChanged.InvokeAsync(match);
    }
}