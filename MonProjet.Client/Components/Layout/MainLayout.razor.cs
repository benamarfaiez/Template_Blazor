using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Localization;
using MonProjet.Client.Models.UI.Menu.NavBar;
using MonProjet.Client.Resources;
using MonProjet.Client.Services;

namespace MonProjet.Client.Components.Layout;

public partial class MainLayout : LayoutComponentBase, IDisposable
{
    [Inject] public IStringLocalizer<SharedResource> Loc { get; set; } = default!;
    [Inject] public ILanguageService LanguageService { get; set; } = default!;
    private ErrorBoundary? _errorBoundary;

    private void Recover() => _errorBoundary?.Recover();

    protected override void OnInitialized()
    {
        LanguageService.OnLanguageChanged += HandleLanguageChanged;
    }

    private void HandleLanguageChanged()
    {
        InvokeAsync(StateHasChanged);
    }
    private List<NavLinkItem> GetNavLinks()
    {
        return [
            new()
            {
                Label = "Platform",
                // Les propriétés de la bannière jaune
                BannerText = "What is Platform? — One Enterprise OS. AI agents that see, decide, and act.",
                BannerLinkText = "Explore",
                BannerLinkUrl = "/platform",
            
                // Configuration du Méga-Menu
                Groups =
                [
                    new()
                    {
                        Title = "AGENTIC AI",
                        Items =
                        [
                            new() { Icon = "🧠", Title = "Astral - Knowledge Graph", Description = "The central brain.", Href = "#" },
                            new() { Icon = "🤖", Title = "AI Agents", Description = "Deploy AI agents for F&P", Badge = "New" },
                            new() { Icon = "🔗", Title = "MCP Connections", Description = "Connect to any system" },
                            new() { Icon = "🎓", Title = "Skills", Description = "AI agent capabilities" }
                        ]
                    },
                    new()
                    {
                        Title = "BUY",
                        Items =
                        [
                            new() { Icon = "🛒", Title = "Intake-to-Pay", Description = "Request to payment, automated", Badge = "Popular" },
                            new() { Icon = "🔄", Title = "Source-to-Pay", Description = "AI-orchestrated S2P suite" },
                            new() { Icon = "📦", Title = "Procure-to-Pay", Description = "P2P across every ERP" }
                        ]
                    },
                    new()
                    {
                        Title = "SELL",
                        Items =
                        [
                            new() { Icon = "💰", Title = "Quote-to-Cash", Description = "Quote to cash, AI-orchestrated" },
                            new() { Icon = "🧾", Title = "Order-to-Cash", Description = "Invoice to collection" }
                        ]
                    },
                    new()
                    {
                        Title = "Treasury",
                        Items =
                        [
                            new() { Icon = "🏦", Title = Loc["Nav_Treasury_History"], Description = Loc["Nav_Treasury_History_Description"], Href = "/treasury/transactions"},
                            new() { Icon = "💳", Title = Loc["Nav_NewTransfer"], Description = Loc["Nav_NewTransfer_Description"], Href = "/treasury/new-transfer" }
                        ]
                    }
                ]
            },
            new() { Label = Loc["Nav_Users"], Href = "/users" },
            new() { Label = Loc["Nav_Gallery"], Href = "/gallery" },
            new() { Label = Loc["Nav_Employees"], Href = "/employees" }
        ];
    }
    public void Dispose()
    {
        LanguageService.OnLanguageChanged -= HandleLanguageChanged;
        GC.SuppressFinalize(this);
    }
}