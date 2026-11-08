namespace MonProjet.Shared.Models.Home;

public record HomeItemDto(
    int Id,
    string Name,
    string Status,
    DateTime CreatedAt);