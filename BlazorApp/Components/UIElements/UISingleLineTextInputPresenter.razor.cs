using BlazorApp.Tool.GUI.Components;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Components.UIElements;

public partial class UISingleLineTextInputPresenter : StyledComponentBase
{
    private UITextInputWrapper _textInputWrapper = default!;

    [Parameter]
    public Tool.GUI.Components.IUISingleLineTextInput UISingleLineTextInput { get; set; } = default!;

    private void OnTextChanged(string newText)
    {
        UISingleLineTextInput.Text(newText);
    }
}
