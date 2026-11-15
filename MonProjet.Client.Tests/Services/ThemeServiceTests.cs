using Microsoft.JSInterop;
using MonProjet.Client.Services;
using NSubstitute;
using Xunit;

namespace MonProjet.Client.Tests.Services;

[Trait("Category", "Unit")]
public sealed class ThemeServiceTests
{
    [Fact]
    public async Task InitializeAsync_LoadsThemeAndRaisesEvent()
    {
        var js = Substitute.For<IJSRuntime>();
        var module = Substitute.For<IJSObjectReference>();

#pragma warning disable CA2012
        js.InvokeAsync<IJSObjectReference>("import", Arg.Any<object?[]?>())
            .Returns(_ => ValueTask.FromResult(module));
        module.InvokeAsync<bool>("getTheme", Arg.Any<object?[]?>())
            .Returns(_ => ValueTask.FromResult(true));
#pragma warning restore CA2012

        var service = new ThemeService(js);
        var changed = 0;
        service.OnThemeChanged += () => changed++;

        await service.InitializeAsync();

        Assert.True(service.IsDarkMode);
        Assert.Equal(1, changed);
    }

    [Fact]
    public async Task ToggleThemeAsync_UpdatesThemeAndRaisesEvent()
    {
        var js = Substitute.For<IJSRuntime>();
        var module = Substitute.For<IJSObjectReference>();

#pragma warning disable CA2012
        js.InvokeAsync<IJSObjectReference>("import", Arg.Any<object?[]?>())
            .Returns(_ => ValueTask.FromResult(module));
#pragma warning restore CA2012

        var service = new ThemeService(js);
        var changed = 0;
        service.OnThemeChanged += () => changed++;

        await service.ToggleThemeAsync();

        Assert.True(service.IsDarkMode);
        Assert.Equal(1, changed);

        Assert.Contains(module.ReceivedCalls(), call =>
        {
            var args = call.GetArguments();
            if (args[0] as string != "setTheme") return false;

            var lastArg = args[args.Length - 1];
            if (lastArg is object?[] paramsArray)
                return Equals(paramsArray[0], true);

            return Equals(lastArg, true);
        });
    }
}