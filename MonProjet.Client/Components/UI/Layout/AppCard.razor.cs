using Microsoft.AspNetCore.Components;
using MonProjet.Client.Components.UI.Core;

namespace MonProjet.Client.Components.UI.Layout;

public partial class AppCard : AppComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
}