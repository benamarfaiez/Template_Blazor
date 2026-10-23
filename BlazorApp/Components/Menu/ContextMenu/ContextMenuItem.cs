using BlazorApp.Components.BasicInput.DropDownButton;

namespace BlazorApp.Components.Menu.ContextMenu;

public sealed class ContextMenuItem : DropDownListItem
{
    internal string? KeyboardShortcut { get; set; } // TODO: Actually handle keyboard shortcuts?
}
