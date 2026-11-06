using MonProjet.Client.Models.UI.Feedback;

namespace MonProjet.Client.Services;

public class ToastService : IToastService
{
    public event Action<ToastMessage>? OnShow;
    public event Action<Guid>? OnRemove;

    public void Show(ToastMessage message) => OnShow?.Invoke(message);

    public void ShowSuccess(string message, string? title = null)
        => Show(new ToastMessage { Message = message, Title = title, Severity = ToastSeverity.Success });

    public void ShowError(string message, string? title = null)
        => Show(new ToastMessage { Message = message, Title = title, Severity = ToastSeverity.Error });

    public void ShowWarning(string message, string? title = null)
        => Show(new ToastMessage { Message = message, Title = title, Severity = ToastSeverity.Warning });

    public void ShowInfo(string message, string? title = null)
        => Show(new ToastMessage { Message = message, Title = title, Severity = ToastSeverity.Info });

    public void Remove(Guid id) => OnRemove?.Invoke(id);
}