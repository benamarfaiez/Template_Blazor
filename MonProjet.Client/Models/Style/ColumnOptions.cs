namespace MonProjet.Client.Models.Style;

public class ColumnOptions
{
    public string? Width { get; set; }
    public ColumnAlignment Alignment { get; set; } = ColumnAlignment.Left;
    public string? CellClass { get; set; }
    public string? HeaderClass { get; set; }

    internal string AlignClass => Alignment switch
    {
        ColumnAlignment.Center => "align-center",
        ColumnAlignment.Right => "align-right",
        _ => "align-left"
    };
}
