using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MonProjet.Client.Components.UI.Core;
using MonProjet.Client.Models.UI;
using MonProjet.Client.Models.UI.Text;

namespace MonProjet.Client.Components.UI.Text;

public partial class MonacoEditor : AppComponentBase, IAsyncDisposable
{
    [Parameter] public string Value { get; set; } = string.Empty;
    [Parameter] public EventCallback<string> ValueChanged { get; set; }
    [Parameter] public string Language { get; set; } = "csharp";
    [Parameter] public MonacoEditorOptions Options { get; set; } = new();
    [Parameter] public string EditorHeight { get; set; } = "400px";

    private ElementReference _container;
    private IJSObjectReference? _module;
    private DotNetObjectReference<MonacoEditor>? _dotNetRef;
    private bool _initialized;
    private string _lastRenderedValue = string.Empty;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender && IsVisible)
        {
            try
            {
                _module = await JSRuntime.InvokeAsync<IJSObjectReference>(
                    "import", "./Components/UI/Text/MonacoEditor.razor.js");

                _dotNetRef = DotNetObjectReference.Create(this);

                await _module.InvokeVoidAsync("init", _container, _dotNetRef, Value, Language, EditorHeight, Options);

                _initialized = true;
                _lastRenderedValue = Value;
            }
            catch (Exception ex) when (ex is JSDisconnectedException or TaskCanceledException or InvalidOperationException)
            {
            }
        }
        else if (_initialized && Value != _lastRenderedValue)
        {
            _lastRenderedValue = Value;
            try
            {
                await _module!.InvokeVoidAsync("setValue", _container, Value);
            }
            catch (JSDisconnectedException) { }
        }
    }

    [JSInvokable]
    public async Task OnEditorChanged(string newValue)
    {
        if (Value == newValue) return;

        Value = newValue;
        _lastRenderedValue = newValue;

        if (ValueChanged.HasDelegate)
        {
            await ValueChanged.InvokeAsync(newValue);
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (_initialized && _module is not null)
        {
            try
            {
                await _module.InvokeVoidAsync("dispose", _container);
                await _module.DisposeAsync();
            }
            catch (JSDisconnectedException) { }
            catch (TaskCanceledException) { }
        }
        _dotNetRef?.Dispose();
        GC.SuppressFinalize(this);
    }
}