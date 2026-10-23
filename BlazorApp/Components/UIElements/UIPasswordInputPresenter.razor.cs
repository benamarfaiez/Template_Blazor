using BlazorApp.Tool.GUI.Components;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Components.UIElements;

public partial class UIPasswordInputPresenter : StyledComponentBase
{
    private UITextInputWrapper _textInputWrapper = default!;

    [Parameter]
    public Tool.GUI.Components.IUIPasswordInput UIPasswordInput { get; set; } = default!;

    private void OnTextChanged(string newText)
    {
        UIPasswordInput.Text(newText);
    }
}
