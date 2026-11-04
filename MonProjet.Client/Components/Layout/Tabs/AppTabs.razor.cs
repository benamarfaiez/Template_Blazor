using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MonProjet.Client.Components.UI.Core;
using MonProjet.Client.Models.Layout;

namespace MonProjet.Client.Components.Layout.Tabs;

public partial class AppTabs : AppComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// L'ID de l'onglet actif (permet la liaison @bind-ActiveTab).
    /// </summary>
    [Parameter] public string? ActiveTab { get; set; }
    [Parameter] public EventCallback<string> ActiveTabChanged { get; set; }

    /// <summary>
    /// Style visuel des onglets.
    /// </summary>
    [Parameter] public TabVariant Variant { get; set; } = TabVariant.Line;

    internal List<TabItem> Tabs { get; } = [];

    internal async Task AddTab(TabItem tab)
    {
        if (Tabs.Any(t => t.Id == tab.Id)) return;

        Tabs.Add(tab);

        // ✅ Auto-sélection du premier onglet si aucun n'est défini
        if (ActiveTab == null && Tabs.Count == 1)
        {
            await SetActiveTab(tab.Id);
        }

        StateHasChanged();
    }

    internal void RemoveTab(TabItem tab)
    {
        Tabs.Remove(tab);
        StateHasChanged();
    }

    private async Task SetActiveTab(string tabId)
    {
        var tab = Tabs.FirstOrDefault(t => t.Id == tabId);
        if (tab == null || tab.IsDisabled) return;

        ActiveTab = tabId;
        if (ActiveTabChanged.HasDelegate)
        {
            await ActiveTabChanged.InvokeAsync(tabId);
        }
    }

    // ✅ Accessibilité : Navigation au clavier (Flèche gauche/droite)
    private async Task HandleKeyDownAsync(KeyboardEventArgs e, int currentIndex)
    {
        if (e.Key == "ArrowRight")
        {
            var next = currentIndex + 1 < Tabs.Count ? currentIndex + 1 : 0;
            await SetActiveTab(Tabs[next].Id);
        }
        else if (e.Key == "ArrowLeft")
        {
            var prev = currentIndex - 1 >= 0 ? currentIndex - 1 : Tabs.Count - 1;
            await SetActiveTab(Tabs[prev].Id);
        }
    }

    private string VariantClass => $"app-tabs-{Variant.ToString().ToLowerInvariant()}";

    public enum TabVariant
    {
        Line,   // Trait en bas (Style classique)
        Pill    // Fond coloré (Style moderne)
    }
}