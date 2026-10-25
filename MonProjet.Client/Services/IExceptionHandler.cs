namespace MonProjet.Client.Services;

public interface IExceptionHandler
{
    Task HandleAsync(Exception ex, string? userMessage = null);
}