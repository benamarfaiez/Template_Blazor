using Microsoft.Extensions.Localization;
using MonProjet.Client.Models.UI.Menu.NavBar;
using MonProjet.Client.Resources;

namespace MonProjet.Client.Services.Navigation;

public sealed class MainNavigationProvider(IStringLocalizer<SharedResource> localizer) : IMainNavigationProvider
{
    public IReadOnlyList<NavLinkItem> CreateLinks() =>
    [
        CreatePlatformLink(),
        new() { Id = "users", Label = localizer["Nav_Users"], Href = "/users" },
        new() { Id = "gallery", Label = localizer["Nav_Gallery"], Href = "/gallery" },
        new() { Id = "employees", Label = localizer["Nav_Employees"], Href = "/employees" }
    ];

    private NavLinkItem CreatePlatformLink() => new()
    {
        Id = "platform",
        Label = "Platform",
        BannerText = "What is Platform? — One Enterprise OS. AI agents that see, decide, and act.",
        BannerLinkText = "Explore",
        BannerLinkUrl = "/platform",
        Groups =
        [
            CreateAgenticAiGroup(),
            CreateBuyGroup(),
            CreateSellGroup(),
            CreateTreasuryGroup()
        ]
    };

    private static MegaMenuGroup CreateAgenticAiGroup() => new()
    {
        Title = "AGENTIC AI",
        Items =
        [
            new() { Icon = "🧠", Title = "Astral - Knowledge Graph", Description = "The central brain.", Href = "#" },
            new() { Icon = "🤖", Title = "AI Agents", Description = "Deploy AI agents for F&P", Badge = "New" },
            new() { Icon = "🔗", Title = "MCP Connections", Description = "Connect to any system" },
            new() { Icon = "🎓", Title = "Skills", Description = "AI agent capabilities" }
        ]
    };

    private static MegaMenuGroup CreateBuyGroup() => new()
    {
        Title = "BUY",
        Items =
        [
            new() { Icon = "🛒", Title = "Intake-to-Pay", Description = "Request to payment, automated", Badge = "Popular" },
            new() { Icon = "🔄", Title = "Source-to-Pay", Description = "AI-orchestrated S2P suite" },
            new() { Icon = "📦", Title = "Procure-to-Pay", Description = "P2P across every ERP" }
        ]
    };

    private static MegaMenuGroup CreateSellGroup() => new()
    {
        Title = "SELL",
        Items =
        [
            new() { Icon = "💰", Title = "Quote-to-Cash", Description = "Quote to cash, AI-orchestrated" },
            new() { Icon = "🧾", Title = "Order-to-Cash", Description = "Invoice to collection" }
        ]
    };

    private MegaMenuGroup CreateTreasuryGroup() => new()
    {
        Title = "Treasury",
        Items =
        [
            new() { Icon = "🏦", Title = localizer["Nav_Treasury_History"], Description = localizer["Nav_Treasury_History_Description"], Href = "/treasury/transactions" },
            new() { Icon = "💳", Title = localizer["Nav_NewTransfer"], Description = localizer["Nav_NewTransfer_Description"], Href = "/treasury/new-transfer" }
        ]
    };
}
