using Microsoft.AspNetCore.Components;
using MonProjet.Client.Components.UI.Core;
using MonProjet.Client.Components.UI.Enums;

namespace MonProjet.Client.Components.UI.BasicInput;

public partial class AppButton : AppComponentBase
{
    [Parameter] public string Text { get; set; } = string.Empty;
    [Parameter] public EventCallback OnClick { get; set; }
    [Parameter] public bool Disabled { get; set; }
    [Parameter] public bool Loading { get; set; }
    [Parameter] public ButtonVariant Variant { get; set; } = ButtonVariant.Primary;

    // Optimisation : Évalué une seule fois par rendu
    private string VariantClass => $"app-button-{Variant.ToString().ToLowerInvariant()}";

    private Task HandleClick()
    {
        return (Disabled || Loading) ? Task.CompletedTask : OnClick.InvokeAsync();
    }

}