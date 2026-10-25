using Microsoft.Extensions.Logging;

namespace MonProjet.Client.Services;

public class ExceptionHandler : IExceptionHandler
{
    private readonly ILogger<ExceptionHandler> _logger;
    public ExceptionHandler(ILogger<ExceptionHandler> logger) => _logger = logger;

    public Task HandleAsync(Exception ex, string? userMessage = null)
    {
        _logger.LogError(ex, "Exception non gÃ©rÃ©e : {Message}", userMessage ?? ex.Message);
        return Task.CompletedTask;
    }
}