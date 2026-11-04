using Microsoft.AspNetCore.Components;

namespace MonProjet.Client.Models.Layout;

internal class TabItem
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string Title { get; set; } = string.Empty;
    public string? Icon { get; set; }
    public bool IsDisabled { get; set; }
    public RenderFragment? Content { get; set; }
}