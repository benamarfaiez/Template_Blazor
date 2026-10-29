namespace MonProjet.Client.Models.Gallery;

public class GalleryViewModel
{
    public List<string> Suggestions { get; set; } =
    [
        "Blazor", "C#", ".NET 10", "ASP.NET Core", "WebAssembly", "MVP"
    ];

    public List<string> SuggestionsLangue { get; set; } =
    [
        "Français", "Anglais", "Espagnol", "Italien", "Portugais", "Néerlandais"
    ];

    public List<DemoItem> Items { get; set; } =
    [
        new(1, "Composant A", "Actif", 120),
        new(2, "Composant B", "Inactif", 45),
        new(3, "Composant C", "En attente", 0)
    ];
}

public record DemoItem(int Id, string Name, string Status, int Count);