namespace MonProjet.Client.Models.NavBar;

public class MegaMenuItem
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public string Href { get; set; } = "#";
    public string? Badge { get; set; }
}