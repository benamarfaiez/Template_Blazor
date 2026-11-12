namespace MonProjet.Client.Models.UI.Menu.NavBar;

public class MegaMenuGroup
{
    public string Title { get; set; } = string.Empty;
    public IReadOnlyList<MegaMenuItem> Items { get; set; } = [];
}