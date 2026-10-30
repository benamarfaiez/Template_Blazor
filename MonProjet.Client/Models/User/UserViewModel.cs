namespace MonProjet.Client.Models.User;

public class UserViewModel
{
    public string Title { get; set; } = "Gestion des Utilisateurs";
    public bool IsLoading { get; set; }
    public string? ErrorMessage { get; set; } = null;
    public List<UserDto> Users { get; set; } = [];
}