using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MonProjet.Client.Components.UI.Core;

namespace MonProjet.Client.Components.UI.BasicInput;

public partial class AppMultiSelect<TItem> : AppComponentBase
{
    [Parameter] public IEnumerable<TItem> Items { get; set; } = [];
    [Parameter] public IEnumerable<TItem> SelectedItems { get; set; } = [];
    [Parameter] public EventCallback<IEnumerable<TItem>> SelectedItemsChanged { get; set; }

    [Parameter] public Func<TItem, string> ValueSelector { get; set; } = i => i?.ToString() ?? "";
    [Parameter] public Func<TItem, string> LabelSelector { get; set; } = i => i?.ToString() ?? "";

    [Parameter] public string Placeholder { get; set; } = "Sélectionnez...";
    [Parameter] public bool Disabled { get; set; }

    private bool _isOpen;
    private List<TItem> _internalSelection = [];

    // Permet de gérer l'état local sans déclencher de boucle infinie avec le parent
    protected override void OnParametersSet()
    {
        _internalSelection = SelectedItems?.ToList() ?? [];
    }

    private void ToggleDropdown()
    {
        if (Disabled) return;
        _isOpen = !_isOpen;
    }

    private async Task ToggleItem(TItem item)
    {
        var itemValue = ValueSelector(item);
        var existing = _internalSelection.FirstOrDefault(x => ValueSelector(x) == itemValue);

        if (existing is not null)
        {
            _internalSelection.Remove(existing);
        }
        else
        {
            _internalSelection.Add(item);
        }

        await SelectedItemsChanged.InvokeAsync(_internalSelection);
    }

    private async Task RemoveItem(TItem item)
    {
        var itemValue = ValueSelector(item);
        var existing = _internalSelection.FirstOrDefault(x => ValueSelector(x) == itemValue);

        if (existing is not null)
        {
            _internalSelection.Remove(existing);
            await SelectedItemsChanged.InvokeAsync(_internalSelection);
        }
    }

    private bool IsSelected(TItem item)
    {
        var itemValue = ValueSelector(item);
        return _internalSelection.Any(x => ValueSelector(x) == itemValue);
    }

    private void HandleFocusOut(FocusEventArgs e)
    {
        _isOpen = false;
    }
}