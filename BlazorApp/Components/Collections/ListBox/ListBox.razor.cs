using BlazorApp.Tool.Extensions;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Components.Collections.ListBox;

public partial class ListBox<TElement> : SelectBase<TElement>, IFocusable where TElement : class
{
    [Parameter]
    public string Role { get; set; } = "listbox";

    [Parameter]
    public bool UseNativeScrollBar { get; set; }

    public ValueTask<bool> FocusAsync()
    {
        return JSRuntime.InvokeVoidWithErrorHandlingAsync("devtoys.DOM.setFocus", Element);
    }
}
