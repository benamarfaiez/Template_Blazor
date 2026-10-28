using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MonProjet.Client.Components.UI.Core;

namespace MonProjet.Client.Components.UI.BasicInput;

public partial class AppDropDownButton : AppComponentBase
{
    [Parameter] public string Text { get; set; } = string.Empty;
    [Parameter] public RenderFragment? ChildContent { get; set; }

    private bool _open;

    private void Toggle()
    {
        _open = !_open;
    }

    private void HandleFocusOut(FocusEventArgs e)
    {
        _open = false;
    }
}