namespace MonProjet.Client.Models.NavBar;
public class NavLinkItem
{
    public string Label { get; set; } = string.Empty;
    public string? Href { get; set; }
    public bool HasDropdown => Groups != null && Groups.Count != 0;
    public List<MegaMenuGroup>? Groups { get; set; }
    public string? BannerText { get; set; }
}
