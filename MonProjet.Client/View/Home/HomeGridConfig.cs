using Microsoft.AspNetCore.Components.Rendering;
using MonProjet.Client.Components.UI.Collections;
using MonProjet.Client.Components.UI.Collections.Column;
using MonProjet.Client.Components.UI.Collections.Helpers;
using MonProjet.Client.Models.Home;
using MonProjet.Client.Models.Style;

namespace MonProjet.Client.View.Home;

/// <summary>
/// Configurer la disposition et le rendu des colonnes pour la page Home.
/// </summary>
public static class HomeGridConfig
{
    public static List<GridColumn<HomeItem>> CreateColumns()
    {
        var columns = new List<GridColumn<HomeItem>>
        {
            // Colonne ID
            GridColumn.Text<HomeItem>(
                header: "ID",
                valueSelector: i => i.Id,
                comparer: (a, b) => a.Id.CompareTo(b.Id)
            ),

            // Colonne Nom
            GridColumn.Text<HomeItem>(
                header: "Nom",
                valueSelector: i => i.Name,
                comparer: (a, b) => string.Compare(a.Name, b.Name, StringComparison.OrdinalIgnoreCase)
            ),

            // Colonne Statut (avec badge)
            GridColumn.Template<HomeItem>(
                header: "Statut",
                template: item => builder => RenderStatusBadge(builder, item.Status),
                comparer: (a, b) => string.Compare(a.Status, b.Status, StringComparison.OrdinalIgnoreCase)
            ),

            // Colonne Date
            GridColumn.Text<HomeItem>(
                header: "Créé le",
                valueSelector: i => i.CreatedAt,
                comparer: (a, b) => a.CreatedAt.CompareTo(b.CreatedAt),
                formatter: raw => raw is DateTime dt ? dt.ToString("dd/MM/yyyy") : "-"
            )
        };

        // Application des styles
        columns[0].Options.Width = "80px";
        columns[0].Options.Alignment = ColumnAlignment.Center;

        columns[2].Options.Width = "140px";
        columns[2].Options.Alignment = ColumnAlignment.Center;

        columns[3].Options.Width = "180px";
        columns[3].Options.Alignment = ColumnAlignment.Right;

        return columns;
    }

    // Extraction de la logique de rendu du badge pour alléger la déclaration de la colonne
    private static void RenderStatusBadge(RenderTreeBuilder builder, string status)
    {
        var (cls, label) = status switch
        {
            "Actif" => ("badge-success", "Actif"),
            "En attente" => ("badge-warning", "En attente"),
            "Terminé" => ("badge-muted", "Terminé"),
            _ => ("badge-info", status)
        };

        builder.OpenElement(0, "span");
        builder.AddAttribute(1, "class", $"badge {cls}");
        builder.AddContent(2, label);
        builder.CloseElement();
    }
}