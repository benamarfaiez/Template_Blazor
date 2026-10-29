using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MonProjet.Client.Components.UI.Collections.Column;
using MonProjet.Client.Components.UI.Core;

namespace MonProjet.Client.Components.UI.Collections;

public partial class AppDataGridBody<TItem> : AppComponentBase
{
    [Parameter] public IEnumerable<TItem> Items { get; set; } = [];
    [Parameter] public List<GridColumn<TItem>> Columns { get; set; } = [];

    // ✅ OCP : Permet de définir une classe CSS conditionnelle pour chaque ligne
    [Parameter] public Func<TItem, string>? RowClassSelector { get; set; }

    // ✅ OCP : Permet de rendre la ligne entière cliquable
    [Parameter] public EventCallback<TItem> OnRowClick { get; set; }

    private async Task HandleRowClickAsync(TItem item)
    {
        if (OnRowClick.HasDelegate)
        {
            await OnRowClick.InvokeAsync(item);
        }
    }
    private async Task HandleKeyDown(KeyboardEventArgs e, TItem item)
    {
        if (!OnRowClick.HasDelegate) return;

        if (e.Key == "Enter" || e.Key == " ")
        {
            await HandleRowClickAsync(item);
        }
    }
}