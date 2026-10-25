namespace MonProjet.Client.Models.User;

public class UserViewModel
{
    public string Title { get; set; } = "Gestion des Utilisateurs";
    public bool IsLoading { get; set; }
    public string? ErrorMessage { get; set; }
    public List<UserDto> Users { get; set; } = [];
}

public record UserDto(int Id, string Name, string Email, string Role);
