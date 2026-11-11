using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Localization;
using MonProjet.Client.Models.UI.Menu.NavBar;
using MonProjet.Client.Resources;

namespace MonProjet.Client.Components.Layout;

public partial class MainLayout : LayoutComponentBase
{
    [Inject]
    public IStringLocalizer<SharedResource> Loc { get; set; } = default!;
    private ErrorBoundary? _errorBoundary;

    private void Recover() => _errorBoundary?.Recover();
    private List<NavLinkItem> _navLinks = [];

    protected override void OnInitialized()
    {
        BuildNavLinks();
    }
    private void BuildNavLinks()
    {
        _navLinks = [
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
                        Title = "FINANCE",
                        Items =
                        [
                            new() { Icon = "🏦", Title = "Finance & Treasury", Description = "Treasury operations & cash flow" },
                            new() { Icon = "💳", Title = "Payments", Description = "B2B payment automation" }
                        ]
                    }
                ]
            },
            new() { Label = Loc["Nav_Users"], Href = "/users" },
            new() { Label = Loc["Nav_Gallery"], Href = "/gallery" },
            new() { Label = Loc["Nav_Employees"], Href = "/employees" },
            new() { Label = Loc["Nav_NewTransfer"], Href = "/treasury/new-transfer" }
        ];
    }
}