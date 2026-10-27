using Microsoft.AspNetCore.Components;
using MonProjet.Client.Components.UI.Core;

namespace MonProjet.Client.Components.UI.Layout;

public partial class AppExpander : AppComponentBase
{
    [Parameter] public string Header { get; set; } = string.Empty;
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public bool OpenOnStart { get; set; }

    private bool _open;

    protected override void OnInitialized()
    {
        _open = OpenOnStart;
    }

    private void Toggle()
    {
        _open = !_open;
    }
}