namespace MonProjet.Shared.Models.Employee;

public record EmployeeDto(
    int Id,
    string Name,
    string Email,
    string Role,
    bool IsActive,
    DateTime HireDate);