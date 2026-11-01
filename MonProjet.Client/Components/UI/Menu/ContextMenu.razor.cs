using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using MonProjet.Client.Components.UI.Core;
using MonProjet.Client.Models.UI.Menu;

namespace MonProjet.Client.Components.UI.Menu;

public partial class ContextMenu : AppComponentBase, IAsyncDisposable
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public List<ContextMenuItem> Items { get; set; } = [];

    private bool _isOpen;
    private double _x;
    private double _y;
    private ElementReference _menuRef;

    private IJSObjectReference? _jsModule;

    private async Task ShowMenuAsync(MouseEventArgs e)
    {
        _x = e.ClientX;
        _y = e.ClientY;
        _isOpen = true;

        await Task.Yield();
        await AdjustPositionAsync();
    }

    private void CloseMenu() => _isOpen = false;

    private async Task HandleItemClickAsync(ContextMenuItem item)
    {
        if (item.IsDisabled || item.IsSeparator) return;

        CloseMenu();

        if (item.OnClick.HasDelegate)
        {
            await item.OnClick.InvokeAsync();
        }
    }

    private void HandleKeyDown(KeyboardEventArgs e)
    {
        if (e.Key == "Escape") CloseMenu();
    }

    private async Task AdjustPositionAsync()
    {
        try
        {
            await _menuRef.FocusAsync();

            _jsModule ??= await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./Components/UI/Menu/ContextMenu.razor.js");
            var dimensions = await _jsModule.InvokeAsync<MenuDimensions>("getContextMenuDimensions", _menuRef);

            var windowWidth = dimensions.WindowWidth;
            var windowHeight = dimensions.WindowHeight;
            var menuWidth = dimensions.MenuWidth;
            var menuHeight = dimensions.MenuHeight;

            if (_x + menuWidth > windowWidth) _x = windowWidth - menuWidth - 8;
            if (_y + menuHeight > windowHeight) _y = windowHeight - menuHeight - 8;

            StateHasChanged();
        }
        catch (JSDisconnectedException) { }
        catch (InvalidOperationException) { /* Ignore : prérendu serveur */ }
    }

    private class MenuDimensions
    {
        public double WindowWidth { get; set; }
        public double WindowHeight { get; set; }
        public double MenuWidth { get; set; }
        public double MenuHeight { get; set; }
    }
    private string MenuPositionStyle => string.Format(
        System.Globalization.CultureInfo.InvariantCulture,
        "left: {0}px; top: {1}px;",
        _x, _y);

    public async ValueTask DisposeAsync()
    {
        if (_jsModule is not null)
        {
            try { await _jsModule.DisposeAsync(); }
            catch (JSDisconnectedException) { }
        }
        GC.SuppressFinalize(this);
    }
}