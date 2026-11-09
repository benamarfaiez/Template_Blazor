using MonProjet.Client.Helpers;

namespace MonProjet.Client.Models.UI.Date;

public class DateRangePickerViewModel
{
    public DateRange CurrentRange { get; private set; } = new(null, null);
    public DateRangePreset CurrentPreset { get; private set; } = DateRangePreset.Custom;
    public string? ErrorMessage { get; private set; }

    public event Action? OnStateChanged;

    public void ApplyPreset(DateRangePreset preset)
    {
        CurrentPreset = preset;
        if (preset != DateRangePreset.Custom)
        {
            CurrentRange = DateRangeHelper.GetPresetRange(preset, DateTime.Today);
            ErrorMessage = null;
        }
        OnStateChanged?.Invoke();
    }

    public void SetDates(DateTime? start, DateTime? end)
    {
        CurrentPreset = DateRangePreset.Custom;
        CurrentRange = new DateRange(start, end);
        ValidateDates();
        OnStateChanged?.Invoke();
    }

    public void Reset()
    {
        CurrentRange = new DateRange(null, null);
        CurrentPreset = DateRangePreset.Custom;
        ErrorMessage = null;
        OnStateChanged?.Invoke();
    }

    private void ValidateDates()
    {
        if (CurrentRange.Start.HasValue && CurrentRange.End.HasValue)
        {
            if (CurrentRange.Start.Value > CurrentRange.End.Value)
            {
                ErrorMessage = "La date de début ne peut pas être supérieure à la date de fin.";
                return;
            }
        }
        ErrorMessage = null;
    }
}
