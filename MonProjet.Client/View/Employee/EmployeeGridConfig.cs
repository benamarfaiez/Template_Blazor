using MonProjet.Client.Components.UI.Collections;
using MonProjet.Client.Components.UI.Collections.Column;
using MonProjet.Client.Components.UI.Helpers;
using MonProjet.Client.Models.Employee;
using MonProjet.Client.Models.Style;

namespace MonProjet.Client.View.Employee;

public static class EmployeeGridConfig
{
    public static List<GridColumn<EmployeeDto>> CreateColumns()
    {
        var columns = new List<GridColumn<EmployeeDto>>
        {
            GridColumn.Text<EmployeeDto>(
                header: "ID",
                valueSelector: e => e.Id,
                comparer: (a, b) => a.Id.CompareTo(b.Id)
            ),

            GridColumn.Text<EmployeeDto>(
                header: "Nom complet",
                valueSelector: e => e.Name,
                comparer: (a, b) => string.Compare(a.Name, b.Name, StringComparison.OrdinalIgnoreCase)
            ),

            GridColumn.Text<EmployeeDto>(
                header: "Email",
                valueSelector: e => e.Email
            ),

            GridColumn.Template<EmployeeDto>(
                header: "Rôle",
                template: emp => builder =>
                {
                    var color = emp.Role == "Admin" ? "#dc2626" : "#2563eb";
                    builder.OpenElement(0, "span");
                    builder.AddAttribute(1, "style", $"color: {color}; font-weight: 600;");
                    builder.AddContent(2, emp.Role);
                    builder.CloseElement();
                },
                comparer: (a, b) => string.Compare(a.Role, b.Role)
            ),

            GridColumn.Text<EmployeeDto>(
                header: "Date d'embauche",
                valueSelector: e => e.HireDate,
                comparer: (a, b) => a.HireDate.CompareTo(b.HireDate),
                formatter: raw => raw is DateTime dt ? dt.ToString("dd/MM/yyyy") : "-"
            )
        };

        // Styles
        columns[0].Options.Width = "80px";
        columns[0].Options.Alignment = ColumnAlignment.Center;
        columns[2].Options.Width = "250px";
        columns[4].Options.Alignment = ColumnAlignment.Right;
        columns[4].Options.Width = "150px";

        return columns;
    }
}