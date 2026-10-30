using Microsoft.AspNetCore.Components;
using MonProjet.Client.Components.UI.Core;
using MonProjet.Client.Components.UI.Enums;

namespace MonProjet.Client.Components.UI.Feedback;

public partial class InfoBar : AppComponentBase
{
    [Parameter] public string Message { get; set; } = string.Empty;
    [Parameter] public InfoSeverity Severity { get; set; } = InfoSeverity.Info;
    [Parameter] public EventCallback OnDismiss { get; set; }

    /// <summary>
    /// Permet de surcharger l'icône par défaut
    /// </summary>
    [Parameter] public string? CustomIcon { get; set; }
    private string SeverityClass => $"app-infobar-{Severity.ToString().ToLowerInvariant()}";
    private string DisplayIcon => CustomIcon ?? Severity switch
    {
        InfoSeverity.Success => "✅",
        InfoSeverity.Warning => "⚠️",
        InfoSeverity.Error => "❌",
        _ => "ℹ️"
    };
    private async Task DismissAsync()
    {
        IsVisible = false;
        await OnDismiss.InvokeAsync();
    }
}