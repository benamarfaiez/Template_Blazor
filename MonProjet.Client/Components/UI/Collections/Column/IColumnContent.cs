using Microsoft.AspNetCore.Components;

namespace MonProjet.Client.Components.UI.Collections.Column;

public interface IColumnContent<TItem>
{
    RenderFragment Render(TItem item);
}
