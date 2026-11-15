using Bunit;
using MonProjet.Client.Components.UI.BasicInput;
using Xunit;

namespace MonProjet.Client.Tests.Components.UI.BasicInput;

[Trait("Category", "Component")]
public sealed class AppDatePickerTests : BunitContext
{
    [Fact]
    public void Click_OpensCalendarAndUpdatesExpandedState()
    {
        var cut = Render<AppDatePicker>(parameters => parameters
            .Add(component => component.Placeholder, "Pick a date"));

        // ✅ CORRECTION : On cible le span de texte à l'intérieur du combobox
        // pour éviter de récupérer l'icône 📅 et les espaces/formats qui l'accompagnent.
        var textSpan = cut.Find("[role='combobox'] .app-datepicker-text");
        Assert.Equal("Pick a date", textSpan.TextContent.Trim());

        var combobox = cut.Find("[role='combobox']");
        Assert.Equal("false", combobox.GetAttribute("aria-expanded"));

        combobox.Click();

        // Vérification post-clic
        Assert.Equal("true", cut.Find("[role='combobox']").GetAttribute("aria-expanded"));
        Assert.NotNull(cut.Find("[role='dialog']"));
    }
}