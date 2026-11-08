namespace MonProjet.Client.Models.UI.Menu.NavBar;

public class NavLinkItem
{
    public string Label { get; set; } = string.Empty;
    public string? Href { get; set; }
    public string? BannerText { get; set; }
    public string? BannerLinkText { get; set; }
    public string? BannerLinkUrl { get; set; }
    public List<MegaMenuGroup>? Groups { get; set; }
    public bool HasDropdown => Groups != null && Groups.Count != 0;
}