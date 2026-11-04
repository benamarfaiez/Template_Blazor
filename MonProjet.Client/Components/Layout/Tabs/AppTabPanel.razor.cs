using Microsoft.AspNetCore.Components;
using MonProjet.Client.Components.UI.Core;
using MonProjet.Client.Models.Layout;

namespace MonProjet.Client.Components.Layout.Tabs;

public partial class AppTabPanel : AppComponentBase, IDisposable
{
    [CascadingParameter] private AppTabs? Parent { get; set; }

    [Parameter] public string Title { get; set; } = string.Empty;
    [Parameter] public string? Icon { get; set; }
    [Parameter] public bool IsDisabled { get; set; }
    [Parameter] public RenderFragment? ChildContent { get; set; }

    private TabItem _tabItem = null!;

    protected override async Task OnInitializedAsync()
    {
        if (Parent == null)
            throw new InvalidOperationException("AppTabPanel doit être placé à l'intérieur d'un AppTabs.");

        _tabItem = new TabItem
        {
            Title = Title,
            Icon = Icon,
            IsDisabled = IsDisabled,
            Content = ChildContent
        };

        await Parent.AddTab(_tabItem);
    }

    // Le composant enfant ne rend rien directement dans le DOM, 
    // c'est le parent qui extrait son contenu.
    public void Dispose()
    {
        Parent?.RemoveTab(_tabItem);
        GC.SuppressFinalize(this);
    }
}