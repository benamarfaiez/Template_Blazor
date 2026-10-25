using Microsoft.AspNetCore.Components;
using MonProjet.Client.Models.Style;

namespace MonProjet.Client.Components.UI.Collections.Column;

public class GridColumn<TItem>(string header, IColumnContent<TItem> content, IColumnSort<TItem>? sort = null)
{
    public string Header { get; set; } = header;

    // Identifiant unique (facultatif, sinon utilise Header)
    public string? Key { get; set; }
    internal string SortKey => Key ?? Header;

    // Composition : délégation des responsabilités
    public ColumnOptions Options { get; set; } = new();
    public IColumnContent<TItem> Content { get; set; } = content;
    public IColumnSort<TItem> Sort { get; set; } = sort ?? new NoSort<TItem>();
}