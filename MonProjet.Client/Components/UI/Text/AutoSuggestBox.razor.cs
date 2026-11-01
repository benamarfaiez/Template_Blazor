using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MonProjet.Client.Components.UI.Core;

namespace MonProjet.Client.Components.UI.Text;

public partial class AutoSuggestBox<TItem> : AppComponentBase
{
    [Parameter] public IEnumerable<TItem>? Items { get; set; }
    [Parameter] public TItem? Value { get; set; }
    [Parameter] public EventCallback<TItem?> ValueChanged { get; set; }

    /// <summary>
    /// Fonction pour extraire le texte à afficher et à rechercher.
    /// </summary>
    [Parameter] public Func<TItem, string> TextSelector { get; set; } = i => i?.ToString() ?? "";

    [Parameter] public string Placeholder { get; set; } = string.Empty;
    [Parameter] public bool Disabled { get; set; }

    /// <summary>
    /// Nombre maximum de suggestions à afficher.
    /// </summary>
    [Parameter] public int MaxSuggestions { get; set; } = 10;

    private bool _isOpen;
    private string _searchText = string.Empty;
    private int _selectedIndex = -1;
    private List<TItem> _filteredItems = [];

    protected override void OnParametersSet()
    {
        if (Value is not null)
        {
            var newText = TextSelector(Value);
            if (_searchText != newText)
            {
                _searchText = newText;
                FilterItems();
            }
        }
        else if (Value is null && !string.IsNullOrEmpty(_searchText))
        {
            _searchText = string.Empty;
        }
    }

    private void FilterItems()
    {
        if (Items is null || string.IsNullOrWhiteSpace(_searchText))
        {
            _filteredItems = Items?.Take(MaxSuggestions).ToList() ?? [];
        }
        else
        {
            _filteredItems = [.. Items
                .Where(i => TextSelector(i).Contains(_searchText, StringComparison.OrdinalIgnoreCase))
                .Take(MaxSuggestions)];
        }
        _selectedIndex = _filteredItems.Count != 0 ? 0 : -1;
    }

    private void OnInput(ChangeEventArgs e)
    {
        _searchText = e.Value?.ToString() ?? string.Empty;
        _isOpen = true;
        FilterItems();
    }

    private void OnFocus()
    {
        _isOpen = true;
        FilterItems();
    }

    private async Task OnBlurAsync()
    {
        await Task.Delay(150);
        _isOpen = false;
    }

    private async Task HandleKeyDownAsync(KeyboardEventArgs e)
    {
        switch (e.Key)
        {
            case "ArrowDown":
                if (!_isOpen) { _isOpen = true; FilterItems(); }
                else if (_selectedIndex < _filteredItems.Count - 1) _selectedIndex++;
                break;

            case "ArrowUp":
                if (_isOpen && _selectedIndex > 0) _selectedIndex--;
                break;

            case "Enter":
                if (_isOpen && _selectedIndex >= 0 && _selectedIndex < _filteredItems.Count)
                {
                    await SelectItemAsync(_filteredItems[_selectedIndex]);
                }
                break;

            case "Escape":
                _isOpen = false;
                break;
        }
    }

    private async Task SelectItemAsync(TItem item)
    {
        _searchText = TextSelector(item);
        _isOpen = false;
        _selectedIndex = -1;
        await ValueChanged.InvokeAsync(item);
    }
}