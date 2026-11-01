using Microsoft.AspNetCore.Components;
namespace MonProjet.Client.Models.UI.Menu;

public class ContextMenuItem
{
    public string Label { get; set; } = string.Empty;
    public string? Icon { get; set; }
    public string? Shortcut { get; set; }
    public bool IsDisabled { get; set; }
    public bool IsSeparator { get; set; }
    public EventCallback OnClick { get; set; }
    public static ContextMenuItem Separator() => new() { IsSeparator = true };
}