using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MonProjet.Client.Services;
using NSubstitute;
using Xunit;

namespace MonProjet.Client.Tests.Services;

[Trait("Category", "Unit")]
public sealed class FocusTrapServiceTests
{
    [Fact]
    public async Task TrapAsync_ImportsModuleAndInvokesTrap()
    {
        var js = Substitute.For<IJSRuntime>();
        var module = Substitute.For<IJSObjectReference>();

#pragma warning disable CA2012
        js.InvokeAsync<IJSObjectReference>("import", Arg.Any<object?[]>())
            .Returns(_ => ValueTask.FromResult(module));
#pragma warning restore CA2012

        var service = new FocusTrapService(js);
        var element = new ElementReference("target");

        await service.TrapAsync(element);

        // Vérifie que la méthode JS "trap" a bien été appelée
        Assert.Contains(module.ReceivedCalls(), call => call.GetArguments()[0] as string == "trap");

        await service.DisposeAsync();
    }
}