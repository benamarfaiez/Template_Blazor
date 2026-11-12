using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Components.Rendering;
using MonProjet.Client.Components.UI.Collections.Column;
using MonProjet.Client.Components.UI.Helpers;
using MonProjet.Client.Models.UI.Style;
using MonProjet.Client.Resources;
using MonProjet.Shared.Models.Home;

namespace MonProjet.Client.View.Home;

/// <summary>
/// Configurer la disposition et le rendu des colonnes pour la page Home.
/// </summary>
public static class HomeGridConfig
{
    public static List<GridColumn<HomeItemDto>> CreateColumns(IStringLocalizer<HomeResource> localizer)
    {
        var columns = new List<GridColumn<HomeItemDto>>
        {
            GridColumn.Text<HomeItemDto>(
                header: localizer["Grid_Id"],
                valueSelector: i => i.Id,
                comparer: (a, b) => a.Id.CompareTo(b.Id)
            ),

            GridColumn.Text<HomeItemDto>(
                header: localizer["Grid_Name"],
                valueSelector: i => i.Name,
                comparer: (a, b) => string.Compare(a.Name, b.Name, StringComparison.OrdinalIgnoreCase)
            ),

            GridColumn.Template<HomeItemDto>(
                header: localizer["Grid_Status"],
                template: item => builder => RenderStatusBadge(builder, item.Status, localizer),
                comparer: (a, b) => string.Compare(a.Status, b.Status, StringComparison.OrdinalIgnoreCase)
            ),

            GridColumn.Text<HomeItemDto>(
                header: localizer["Grid_CreatedOn"],
                valueSelector: i => i.CreatedAt,
                comparer: (a, b) => a.CreatedAt.CompareTo(b.CreatedAt),
                formatter: raw => raw is DateTime dt ? dt.ToString("d") : "-"
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
    private static void RenderStatusBadge(RenderTreeBuilder builder, string status, IStringLocalizer<HomeResource> localizer)
    {
        var (cls, label) = status switch
        {
            "Actif" or "Active" => ("badge-success", localizer["Status_Active"].Value),
            "En attente" or "Pending" => ("badge-warning", localizer["Status_Pending"].Value),
            "Terminé" or "Completed" => ("badge-muted", localizer["Status_Completed"].Value),
            _ => ("badge-info", status)
        };

        builder.OpenElement(0, "span");
        builder.AddAttribute(1, "class", $"badge {cls}");
        builder.AddContent(2, label);
        builder.CloseElement();
    }
}