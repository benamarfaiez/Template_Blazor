namespace MonProjet.Client.Models.UI.Feedback;
public class ToastMessage
{
    public Guid Id { get; } = Guid.NewGuid();
    public string Message { get; set; } = string.Empty;
    public string? Title { get; set; }
    public ToastSeverity Severity { get; set; }
    public int DurationMs { get; set; } = 4000;
    public bool ShowProgressBar { get; set; } = true;
}