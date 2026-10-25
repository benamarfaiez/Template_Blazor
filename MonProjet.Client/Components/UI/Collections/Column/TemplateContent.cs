using Microsoft.AspNetCore.Components;

namespace MonProjet.Client.Components.UI.Collections.Column;

public class TemplateContent<TItem>(RenderFragment<TItem> template) : IColumnContent<TItem>
{
    private readonly RenderFragment<TItem> _template = template;

    public RenderFragment Render(TItem item) => _template(item);
}
