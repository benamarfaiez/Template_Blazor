using BlazorApp.Tool.GUI.Components;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Components.UIElements;

public partial class UIInfoBarPresenter : ComponentBase
{
    [Parameter]
    public Tool.GUI.Components.IUIInfoBar UIInfoBar { get; set; } = default!;

    internal async Task OnCloseAsync()
    {
        Guard.IsNotNull(UIInfoBar);
        if (UIInfoBar.OnCloseAction is not null)
        {
            await UIInfoBar.OnCloseAction.Invoke(); // TODO: Try Catch and log?
        }

        UIInfoBar.Close();
    }

    internal async Task OnActionButtonClickAsync()
    {
        Guard.IsNotNull(UIInfoBar);
        if (UIInfoBar.OnActionButtonClick is not null)
        {
            await UIInfoBar.OnActionButtonClick.Invoke(); // TODO: Try Catch and log?
        }
    }

    private InfoBarSeverity GetSeverity()
    {
        return UIInfoBar.Severity switch
        {
            UIInfoBarSeverity.Informational => InfoBarSeverity.Informational,
            UIInfoBarSeverity.Error => InfoBarSeverity.Error,
            UIInfoBarSeverity.Success => InfoBarSeverity.Success,
            UIInfoBarSeverity.Warning => InfoBarSeverity.Warning,
            _ => InfoBarSeverity.Informational,
        };
    }
}
