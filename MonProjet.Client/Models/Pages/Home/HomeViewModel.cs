using MonProjet.Shared.Models.Home;

namespace MonProjet.Client.Models.Pages.Home;

public class HomeViewModel
{
    public string Title { get; set; } = "Tableau de bord";
    public bool IsLoading { get; set; }
    public string? ErrorMessage { get; set; } = null;
    public List<HomeItemDto> Items { get; set; } = [];
}
