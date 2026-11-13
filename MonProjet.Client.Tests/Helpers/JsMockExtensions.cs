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
        // ✅ 1. Intercepte la surcharge SANS CancellationToken
        module.InvokeAsync<T>(Arg.Any<string>(), Arg.Any<object?[]?>())
            .Returns(_ => ValueTask.FromResult(value));

        // ✅ 2. Intercepte la surcharge AVEC CancellationToken
        module.InvokeAsync<T>(Arg.Any<string>(), Arg.Any<CancellationToken>(), Arg.Any<object?[]?>())
            .Returns(_ => ValueTask.FromResult(value));
#pragma warning restore CA2012
    }
}