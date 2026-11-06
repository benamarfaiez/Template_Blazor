using Microsoft.AspNetCore.Components;

namespace MonProjet.Client.Components.UI.Stepper;
internal class StepItem
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string Title { get; set; } = string.Empty;
    public string? Subtitle { get; set; }
    public string? Icon { get; set; }
    public bool IsDisabled { get; set; }
    public RenderFragment? Content { get; set; }
}