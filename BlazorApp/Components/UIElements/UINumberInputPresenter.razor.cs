using BlazorApp.Tool.GUI.Components;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Components.UIElements;

public partial class UINumberInputPresenter : StyledComponentBase
{
    private UITextInputWrapper _textInputWrapper = default!;

    [Parameter]
    public Tool.GUI.Components.IUINumberInput UINumberInput { get; set; } = default!;

    private void OnTextChanged(string newText)
    {
        UINumberInput.Text(newText);
    }
}
