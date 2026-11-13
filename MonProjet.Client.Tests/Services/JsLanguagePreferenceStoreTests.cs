using Microsoft.JSInterop;
using MonProjet.Client.Services;
using MonProjet.Client.Tests.Helpers;
using NSubstitute;
using Xunit;

namespace MonProjet.Client.Tests.Services;

[Trait("Category", "Unit")]
public sealed class JsLanguagePreferenceStoreTests
{
    [Fact]
    public async Task GetSavedCultureAsync_ImportsModuleAndReadsLanguage()
    {
        var js = Substitute.For<IJSRuntime>();
        var module = Substitute.For<IJSObjectReference>();

        js.SetupModuleImport(module);
        module.SetupModuleReturn<string?>("getLanguage", "fr-FR");

        var store = new JsLanguagePreferenceStore(js);

        var result = await store.GetSavedCultureAsync();

        Assert.Equal("fr-FR", result);

        await js.Received(1).InvokeAsync<IJSObjectReference>(
            "import",
            Arg.Is<object?[]>(args => Equals(args[0], "./js/language.js")));

        await store.DisposeAsync();
    }

    [Fact]
    public async Task SaveCultureAsync_InvokesJavascriptWithCultureCode()
    {
        var js = Substitute.For<IJSRuntime>();
        var module = Substitute.For<IJSObjectReference>();

        js.SetupModuleImport(module);

        var store = new JsLanguagePreferenceStore(js);

        await store.SaveCultureAsync("en-US");

        Assert.Contains(module.ReceivedCalls(), call =>
        {
            var args = call.GetArguments();
            if (args[0] as string != "setLanguage") return false;

            var lastArg = args[args.Length - 1];
            if (lastArg is object?[] paramsArray)
            {
                return Equals(paramsArray[0], "en-US");
            }
            return Equals(lastArg, "en-US");
        });

        await store.DisposeAsync();
    }
}