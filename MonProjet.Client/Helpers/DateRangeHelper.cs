using MonProjet.Client.Models.UI.Date;

namespace MonProjet.Client.Helpers;

public static class DateRangeHelper
{
    public static DateRange GetPresetRange(DateRangePreset preset, DateTime today)
    {
        var date = today.Date;

        return preset switch
        {
            DateRangePreset.Today => new DateRange(date, date),

            DateRangePreset.Last7Days => new DateRange(date.AddDays(-6), date),

            DateRangePreset.Last30Days => new DateRange(date.AddDays(-29), date),

            DateRangePreset.ThisMonth => new DateRange(
                new DateTime(date.Year, date.Month, 1),
                new DateTime(date.Year, date.Month, 1).AddMonths(1).AddDays(-1)
            ),

            DateRangePreset.LastMonth => new DateRange(
                new DateTime(date.Year, date.Month, 1).AddMonths(-1),
                new DateTime(date.Year, date.Month, 1).AddDays(-1)
            ),

            DateRangePreset.ThisYear => new DateRange(
                new DateTime(date.Year, 1, 1),
                new DateTime(date.Year, 12, 31)
            ),

            _ => new DateRange(null, null)
        };
    }
}