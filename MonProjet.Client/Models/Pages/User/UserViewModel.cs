using MonProjet.Shared.Models.User;

namespace MonProjet.Client.Models.Pages.User;

public class UserViewModel
{
    public string Title { get; set; } = "Gestion des Utilisateurs";
    public bool IsLoading { get; set; }
    public string? ErrorMessage { get; set; }
    private readonly List<UserDto> _users = [];
    public IReadOnlyList<UserDto> Users => _users;
    public void SetUsers(IEnumerable<UserDto> users)
    {
        _users.Clear();
        _users.AddRange(users);
    }
}