using Microsoft.AspNetCore.Components.Rendering;
using MonProjet.Client.Components.UI.Collections.Column;
using MonProjet.Client.Components.UI.Helpers;
using MonProjet.Client.Models.Style;
using MonProjet.Client.Models.User;

namespace MonProjet.Client.View.User;

/// <summary>
/// Responsabilité unique : Configurer les colonnes du tableau des Utilisateurs.
/// </summary>
public static class UserGridConfig
{
    public static List<GridColumn<UserDto>> CreateColumns()
    {
        var columns = new List<GridColumn<UserDto>>
        {
            // 1. Colonne simple avec tri
            GridColumn.Text<UserDto>(
                header: "ID",
                valueSelector: u => u.Id,
                comparer: (a, b) => a.Id.CompareTo(b.Id)
            ),

            // 2. Colonne avec formatage
            GridColumn.Text<UserDto>(
                header: "Nom",
                valueSelector: u => u.Name,
                comparer: (a, b) => string.Compare(a.Name, b.Name, StringComparison.OrdinalIgnoreCase)
            ),

            // 3. Colonne avec Template personnalisé (Badge de rôle)
            GridColumn.Template<UserDto>(
                header: "Rôle",
                template: user => builder => RenderRoleBadge(builder, user.Role)
            )
        };

        // Configuration des styles
        columns[0].Options.Width = "80px";
        columns[0].Options.Alignment = ColumnAlignment.Center;

        columns[2].Options.Width = "120px";
        columns[2].Options.Alignment = ColumnAlignment.Center;

        return columns;
    }

    public static List<UserDto> GetSampleUsers()
    {
        return
            [
                new UserDto(1, "Alice", "alice@example.com", "Admin"),
                new UserDto(2, "Bob", "bob@example.com", "Éditeur"),
                new UserDto(3, "Charlie", "charlie@example.com", "Utilisateur"),
                new UserDto(4, "Diana", "diana@example.com", "Admin"),
                new UserDto(5, "Eve", "eve@example.com", "Éditeur")
            ];
    }
    // Extraction de la logique de rendu (RenderTreeBuilder) pour alléger la déclaration
    private static void RenderRoleBadge(RenderTreeBuilder builder, string role)
    {
        var (color, bg) = role switch
        {
            "Admin" => ("#dc2626", "#fee2e2"),
            "Éditeur" => ("#2563eb", "#dbeafe"),
            _ => ("#64748b", "#f1f5f9")
        };

        builder.OpenElement(0, "span");
        builder.AddAttribute(1, "style", $"color: {color}; background: {bg}; padding: 2px 8px; border-radius: 12px; font-weight: 600; font-size: 12px;");
        builder.AddContent(2, role);
        builder.CloseElement();
    }
}