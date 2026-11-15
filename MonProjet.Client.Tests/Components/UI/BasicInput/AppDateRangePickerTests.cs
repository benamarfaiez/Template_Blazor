using Bunit;
using MonProjet.Client.Components.UI.BasicInput;
using MonProjet.Client.Models.UI.Date;
using Xunit;

namespace MonProjet.Client.Tests.Components.UI.BasicInput;

[Trait("Category", "Component")]
public sealed class AppDateRangePickerTests : BunitContext
{
    [Fact]
    public void ResetButton_EmitsEmptyDateRange()
    {
        DateRange? changedRange = null;

        var cut = Render<AppDateRangePicker>(parameters => parameters
            .Add(component => component.InitialRange, new DateRange(DateTime.Today.AddDays(-7), DateTime.Today))
            .Add(component => component.OnRangeChanged, range => changedRange = range));

        cut.Find("button[aria-label='Réinitialiser les dates']").Click();

        Assert.NotNull(changedRange);
        Assert.Null(changedRange.Start);
        Assert.Null(changedRange.End);
    }
}