using MonProjet.Client.Models.UI.Files;
using MonProjet.Shared.Models.Gallery;

namespace MonProjet.Client.Models.Pages.Gallery;

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

    public List<DemoItemDto> Items { get; set; } =
    [
        new(1, "Composant A", "Actif", 120),
        new(2, "Composant B", "Inactif", 45),
        new(3, "Composant C", "En attente", 0)
    ];

    public List<ClientFileInfo> UploadedFiles { get; set; } = [];
    public bool IsUploading { get; set; }
}