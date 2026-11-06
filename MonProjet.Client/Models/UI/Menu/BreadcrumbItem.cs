namespace MonProjet.Client.Models.UI.Menu;

public class BreadcrumbItem
{
    public string Label { get; set; } = string.Empty;
    public string? Href { get; set; }
    public string? Icon { get; set; }
    public bool IsDisabled { get; set; }

    public BreadcrumbItem() { }

    public BreadcrumbItem(string label, string? href = null, string? icon = null)
    {
        Label = label;
        Href = href;
        Icon = icon;
    }
}