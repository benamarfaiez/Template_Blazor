using Microsoft.AspNetCore.Components;
using MonProjet.Client.Components.UI.Core;
using MonProjet.Client.Models.UI;
using MonProjet.Client.Models.UI.Feedback;
using MonProjet.Client.Services;

namespace MonProjet.Client.Components.UI.Feedback;

public partial class AppToast : AppComponentBase, IDisposable
{
    [Inject] private IToastService ToastService { get; set; } = default!;

    private readonly List<ToastMessage> _toasts = [];

    protected override void OnInitialized()
    {
        ToastService.OnShow += HandleShow;
        ToastService.OnRemove += HandleRemove;
    }

    private async void HandleShow(ToastMessage toast)
    {
        // ✅ SÉCURITÉ : On s'assure d'être sur le thread de l'UI
        await InvokeAsync(() =>
        {
            // Limite à 5 toasts maximum pour ne pas envahir l'écran
            if (_toasts.Count >= 5) _toasts.RemoveAt(0);

            _toasts.Add(toast);
            StateHasChanged();
        });

        // Auto-dismiss
        _ = Task.Delay(toast.DurationMs).ContinueWith(_ => HandleRemove(toast.Id));
    }

    private async void HandleRemove(Guid id)
    {
        await InvokeAsync(() =>
        {
            var toast = _toasts.FirstOrDefault(t => t.Id == id);
            if (toast != null)
            {
                _toasts.Remove(toast);
                StateHasChanged();
            }
        });
    }

    private void Close(ToastMessage toast) => HandleRemove(toast.Id);

    private static string GetIcon(ToastSeverity severity) => severity switch
    {
        ToastSeverity.Success => "✅",
        ToastSeverity.Error => "❌",
        ToastSeverity.Warning => "⚠️",
        _ => "ℹ️"
    };

    public void Dispose()
    {
        ToastService.OnShow -= HandleShow;
        ToastService.OnRemove -= HandleRemove;
        GC.SuppressFinalize(this);
    }
}