using Microsoft.AspNetCore.Components;
using MonProjet.Client.Components.UI.Core;
using MonProjet.Client.Components.UI.Enums.Text;
using System.Text;

namespace MonProjet.Client.Components.UI.Text;

public partial class TextBlock : AppComponentBase
{
    [Parameter] public string Text { get; set; } = string.Empty;
    [Parameter] public TextVariant Variant { get; set; } = TextVariant.Normal;
    [Parameter] public TextElement As { get; set; } = TextElement.Span;
    [Parameter] public TextAlignment Align { get; set; } = TextAlignment.Left;
    [Parameter] public bool Truncate { get; set; }
    [Parameter] public int? LineClamp { get; set; }

    private string? _finalCssClasses = string.Empty;
    private string _finalStyles = string.Empty;
    private Dictionary<string, object> _mergedAttributes = [];
    private string? TitleAttribute => (Truncate || (LineClamp.HasValue && LineClamp > 0)) ? Text : null;

    protected override void OnParametersSet()
    {
        BuildCssClasses();
        BuildStyles();
        MergeAttributes();
        base.OnParametersSet();
    }

    private void BuildCssClasses()
    {
        _finalCssClasses = ClassNames(
            "text-block",
            $"text-block-{Variant.ToString().ToLowerInvariant()}",
            Truncate ? "is-truncated" : null,
            AdditionalAttributes?["class"]?.ToString()
        );
    }

    private void BuildStyles()
    {
        var sb = new StringBuilder();

        if (Align != TextAlignment.Left)
            sb.Append($"text-align: {Align.ToString().ToLowerInvariant()}; ");

        if (!string.IsNullOrEmpty(Width)) sb.Append($"width: {Width}; ");
        if (!string.IsNullOrEmpty(MarginLeft)) sb.Append($"margin-left: {MarginLeft}; ");
        if (!string.IsNullOrEmpty(MarginRight)) sb.Append($"margin-right: {MarginRight}; ");
        if (!string.IsNullOrEmpty(MarginTop)) sb.Append($"margin-top: {MarginTop}; ");
        if (!string.IsNullOrEmpty(MarginBottom)) sb.Append($"margin-bottom: {MarginBottom}; ");

        if (!string.IsNullOrEmpty(PaddingLeft)) sb.Append($"padding-left: {PaddingLeft}; ");
        if (!string.IsNullOrEmpty(PaddingRight)) sb.Append($"padding-right: {PaddingRight}; ");
        if (!string.IsNullOrEmpty(PaddingTop)) sb.Append($"padding-top: {PaddingTop}; ");
        if (!string.IsNullOrEmpty(PaddingBottom)) sb.Append($"padding-bottom: {PaddingBottom}; ");

        if (LineClamp.HasValue && LineClamp.Value > 0)
        {
            sb.Append($"display: -webkit-box; -webkit-line-clamp: {LineClamp.Value}; -webkit-box-orient: vertical; overflow: hidden; ");
        }

        var userStyle = AdditionalAttributes?["style"]?.ToString();
        if (!string.IsNullOrEmpty(userStyle)) sb.Append(userStyle);

        _finalStyles = sb.ToString().TrimEnd();
    }

    private void MergeAttributes()
    {
        if (AdditionalAttributes is null)
        {
            _mergedAttributes.Clear();
            return;
        }

        _mergedAttributes = AdditionalAttributes
            .Where(a => a.Key != "class" && a.Key != "style")
            .ToDictionary(a => a.Key, a => a.Value);

        if (TitleAttribute is not null)
        {
            _mergedAttributes["title"] = TitleAttribute;
        }
    }

}