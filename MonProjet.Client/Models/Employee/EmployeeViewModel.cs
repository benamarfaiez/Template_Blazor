namespace MonProjet.Client.Models.Employee
{
    public class EmployeeViewModel
    {
        public string Title { get; set; } = "Annuaire de l'entreprise";
        public bool IsLoading { get; set; }
        public string? ErrorMessage { get; set; } = null;
        public List<EmployeeDto> Employees { get; set; } = [];
    }
}
