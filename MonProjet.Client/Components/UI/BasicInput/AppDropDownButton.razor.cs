using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using MonProjet.Client.Components.UI.Core;
using MonProjet.Client.Components.UI.Enums;

namespace MonProjet.Client.Components.UI.BasicInput;

public partial class AppDropDownButton : AppComponentBase, IAsyncDisposable
{
    [Inject] private IJSRuntime JS { get; set; } = default!;

    [Parameter] public string Text { get; set; } = string.Empty;
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public bool IsOpen { get; set; }
    [Parameter] public EventCallback<bool> IsOpenChanged { get; set; }
    [Parameter] public ButtonVariant Variant { get; set; } = ButtonVariant.Primary;
    [Parameter] public bool TriggerOnHover { get; set; } = false;
    private CancellationTokenSource? _hoverCts;
    private ElementReference _dropdownElement;
    private IJSObjectReference? _focusOutModule;
    private DotNetObjectReference<AppDropDownButton>? _selfReference;
    private bool _disposed;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            if (_disposed)
            {
                return;
            }

            var focusOutModule = await JS.InvokeAsync<IJSObjectReference>("import", "./js/dropdown.js");
            if (_disposed)
            {
                try
                {
                    await focusOutModule.DisposeAsync();
                }
                catch (JSDisconnectedException)
                {
                }

                return;
            }

            _focusOutModule = focusOutModule;
            _selfReference = DotNetObjectReference.Create(this);
            await _focusOutModule.InvokeVoidAsync("registerFocusOut", _dropdownElement, _selfReference);
        }
    }

    private async Task ToggleAsync()
    {
        if (TriggerOnHover && IsOpen)
        {
            return;
        }

        IsOpen = !IsOpen;
        await IsOpenChanged.InvokeAsync(IsOpen);
    }

    private async Task HandleKeyDownAsync(KeyboardEventArgs args)
    {
        if (args.Key == "Escape" && IsOpen)
        {
            await CloseAsync();
        }
    }

    [JSInvokable]
    public async Task HandleFocusOutAsync()
    {
        if (!IsOpen)
        {
            return;
        }

        await CloseAsync();
        await InvokeAsync(StateHasChanged);
    }


    private async Task CloseAsync()
    {
        IsOpen = false;
        await IsOpenChanged.InvokeAsync(IsOpen);
    }
    private async Task OnMouseEnterAsync()
    {
        if (!TriggerOnHover) return;

        _hoverCts?.Cancel();
        if (!IsOpen)
        {
            IsOpen = true;
            await IsOpenChanged.InvokeAsync(IsOpen);
        }
    }

    private async Task OnMouseLeaveAsync()
    {
        if (!TriggerOnHover) return;

        _hoverCts?.Cancel();
        _hoverCts = new CancellationTokenSource();

        try
        {
            await Task.Delay(150, _hoverCts.Token);
            if (IsOpen)
            {
                await CloseAsync();
            }
        }
        catch (TaskCanceledException)
        {
        }
    }

    public async ValueTask DisposeAsync()
    {
        _disposed = true;
        _hoverCts?.Cancel();
        _hoverCts?.Dispose();
        _selfReference?.Dispose();

        if (_focusOutModule is not null)
        {
            try
            {
                await _focusOutModule.DisposeAsync();
            }
            catch (JSDisconnectedException)
            {
            }
        }
        GC.SuppressFinalize(this);
    }
}