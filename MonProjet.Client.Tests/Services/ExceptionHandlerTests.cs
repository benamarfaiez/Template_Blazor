using Microsoft.Extensions.Logging;
using MonProjet.Client.Services;
using NSubstitute;
using Xunit;

namespace MonProjet.Client.Tests.Services;

[Trait("Category", "Unit")]
public sealed class ExceptionHandlerTests
{
    [Fact]
    public async Task HandleAsync_LogsExceptionAndUserMessage()
    {
        var logger = Substitute.For<ILogger<ExceptionHandler>>();
        var handler = new ExceptionHandler(logger);
        var exception = new InvalidOperationException("Failure");

        await handler.HandleAsync(exception, "Friendly message");

        var logCall = Assert.Single(logger.ReceivedCalls(), call => call.GetMethodInfo().Name == "Log");
        var arguments = logCall.GetArguments();
        Assert.Equal(LogLevel.Error, arguments[0]);
        Assert.Contains("Friendly message", arguments[2]!.ToString());
        Assert.Same(exception, arguments[3]);
    }
}
