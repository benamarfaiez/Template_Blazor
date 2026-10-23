namespace BlazorApp.Components;

internal interface IFocusable
{
    ValueTask<bool> FocusAsync();
}
