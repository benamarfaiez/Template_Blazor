using BlazorApp.Tool.GUI.Components;
using BlazorApp.Tool.Threading;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Components.UIElements;

public partial class UIMultiLineTextInputPresenter : JSStyledComponentBase
{
    private readonly TaskCompletionSource _monacoInitializationAwaiter = new();
    private readonly DisposableSemaphore _semaphore = new();

    private UITextInputWrapper _textInputWrapper = default!;
    private bool _ignoreChangeComingFromUIMultiLineTextInput;
    private bool _ignoreSelectionComingFromUIMultiLineTextInput;

    internal string TextModelName { get; private set; } = string.Empty;

    [Parameter]
    public Tool.GUI.Components.IUIMultiLineTextInput UIMultiLineTextInput { get; set; } = default!;

    protected override void OnInitialized()
    {
        base.OnInitialized();
        TextModelName = Id + "-text-model";

        UIMultilineTextInputHelper.RegisterMultiLineTextInputPresenter(this);

        UIMultiLineTextInput.SyntaxColorizationLanguageNameChanged += UIMultiLineTextInput_SyntaxColorizationLanguageNameChanged;
        UIMultiLineTextInput.TextChanged += UIMultiLineTextInput_TextChanged;
    }

    public override ValueTask DisposeAsync()
    {
        UIMultiLineTextInput.SyntaxColorizationLanguageNameChanged -= UIMultiLineTextInput_SyntaxColorizationLanguageNameChanged;
        UIMultiLineTextInput.TextChanged -= UIMultiLineTextInput_TextChanged;

        UIMultilineTextInputHelper.UnregisterMultiLineTextInputPresenter(this);

        return base.DisposeAsync();
    }

    private async void UIMultiLineTextInput_TextChanged(object? sender, EventArgs e)
    {
        using (await _semaphore.WaitAsync(CancellationToken.None))
        {
            if (!_ignoreChangeComingFromUIMultiLineTextInput)
            {
                await _monacoInitializationAwaiter.Task;
                await _monacoEditor.SetValueAsync(UIMultiLineTextInput.Text);
            }
        }
    }

    private async void UIMultiLineTextInput_SyntaxColorizationLanguageNameChanged(object? sender, EventArgs e)
    {
        await _monacoInitializationAwaiter.Task;
        await _monacoEditor.SetLanguageAsync(UIMultiLineTextInput.SyntaxColorizationLanguageName);

        // Start the language service, if necessary.
        await MonacoLanguageService.RegisterLanguageAsync(UIMultiLineTextInput.SyntaxColorizationLanguageName);
    }

    private static string GetClassNameForHighlightedTextSpan(Tool.GUI.Components.UIHighlightedTextSpan highlightedTextSpan)
    {
        return highlightedTextSpan.Color switch
        {
            UIHighlightedTextSpanColor.Default => "ui-multiline-text-input-highlighted-text-span-default",
            UIHighlightedTextSpanColor.Blue => "ui-multiline-text-input-highlighted-text-span-blue",
            UIHighlightedTextSpanColor.Green => "ui-multiline-text-input-highlighted-text-span-green",
            UIHighlightedTextSpanColor.Red => "ui-multiline-text-input-highlighted-text-span-red",
            UIHighlightedTextSpanColor.Yellow => "ui-multiline-text-input-highlighted-text-span-yellow",
            UIHighlightedTextSpanColor.Purple => "ui-multiline-text-input-highlighted-text-span-purple",
            UIHighlightedTextSpanColor.Teal => "ui-multiline-text-input-highlighted-text-span-teal",
            _ => throw new NotImplementedException()
        };
    }
}
