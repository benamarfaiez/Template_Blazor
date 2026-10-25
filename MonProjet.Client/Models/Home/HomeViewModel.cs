namespace MonProjet.Client.Models.Home;

public class HomeViewModel
{
    public string Title { get; set; } = "Tableau de bord";
    public bool IsLoading { get; set; }
    public string? ErrorMessage { get; set; }
    public List<HomeItem> Items { get; set; } = [];
}

public record HomeItem(int Id, string Name, string Status, DateTime CreatedAt);