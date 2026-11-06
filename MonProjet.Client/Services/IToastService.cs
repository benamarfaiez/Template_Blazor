using MonProjet.Client.Models.UI.Feedback;

namespace MonProjet.Client.Services;

public interface IToastService
{
    event Action<ToastMessage>? OnShow;
    event Action<Guid>? OnRemove;

    void Show(ToastMessage message);
    void ShowSuccess(string message, string? title = null);
    void ShowError(string message, string? title = null);
    void ShowWarning(string message, string? title = null);
    void ShowInfo(string message, string? title = null);
    void Remove(Guid id);
}