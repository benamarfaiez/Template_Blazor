namespace MonProjet.Client.Models.UI.Text;

public class MonacoEditorOptions
{
    public bool ReadOnly { get; set; } = false;
    public bool WordWrap { get; set; } = true;
    public bool MinimapEnabled { get; set; } = false;
    public bool LineNumbersVisible { get; set; } = true;
    public bool AutoFormat { get; set; } = true;
    public string Theme { get; set; } = "vs"; // "vs", "vs-dark", "hc-black"
}
