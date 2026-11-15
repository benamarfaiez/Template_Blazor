using Microsoft.JSInterop;
using NSubstitute;

namespace MonProjet.Client.Tests.Helpers;

public static class JsMockExtensions
{
    public static void SetupModuleImport(this IJSRuntime js, IJSObjectReference module)
    {
#pragma warning disable CA2012
        js.InvokeAsync<IJSObjectReference>("import", Arg.Any<object?[]>())
            .Returns(_ => ValueTask.FromResult(module));
#pragma warning restore CA2012
    }

    public static void SetupModuleReturn<T>(this IJSObjectReference module, string method, T value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(method);
#pragma warning disable CA2012
        module.InvokeAsync<T>(method, Arg.Any<object?[]?>())
            .Returns(_ => ValueTask.FromResult(value));

        module.InvokeAsync<T>(method, Arg.Any<CancellationToken>(), Arg.Any<object?[]?>())
            .Returns(_ => ValueTask.FromResult(value));
#pragma warning restore CA2012
    }
}