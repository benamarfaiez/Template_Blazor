using System.Globalization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MonProjet.Client.Components.UI.Core;

namespace MonProjet.Client.Components.UI.BasicInput;

public partial class AppDatePicker : AppComponentBase
{
    [Parameter] public DateTime? Value { get; set; }
    [Parameter] public EventCallback<DateTime?> ValueChanged { get; set; }
    [Parameter] public DateTime? Min { get; set; }
    [Parameter] public DateTime? Max { get; set; }
    [Parameter] public string Placeholder { get; set; } = "Sélectionner une date...";
    [Parameter] public bool Disabled { get; set; }

    private bool _isOpen;
    private DateTime _viewDate = DateTime.Today;
    private List<DateTime?> _cachedDays = [];
    private DateTime _lastRenderedMonth = DateTime.MinValue;

    private readonly string[] _dayNames = ["Lu", "Ma", "Me", "Je", "Ve", "Sa", "Di"];
    private string DisplayValue => Value?.ToString("dd/MM/yyyy") ?? string.Empty;

    protected override void OnParametersSet()
    {
        if (Value.HasValue && !_isOpen && Value.Value.Month != _viewDate.Month)
        {
            _viewDate = Value.Value;
        }
    }

    private void ToggleCalendar()
    {
        if (Disabled) return;
        _isOpen = !_isOpen;

        if (_isOpen && Value.HasValue)
        {
            _viewDate = Value.Value;
        }
    }

    private void HandleFocusOut(FocusEventArgs e)
    {
        _isOpen = false;
    }

    private void PreviousMonth() => _viewDate = _viewDate.AddMonths(-1);
    private void NextMonth() => _viewDate = _viewDate.AddMonths(1);

    private async Task SelectDate(DateTime date)
    {
        if (IsDisabled(date)) return;

        Value = date;
        _isOpen = false;
        await ValueChanged.InvokeAsync(Value);
    }

    private List<DateTime?> GetDaysInMonth()
    {
        if (_lastRenderedMonth == _viewDate && _cachedDays.Count > 0)
        {
            return _cachedDays;
        }

        var days = new List<DateTime?>();
        var firstDayOfMonth = new DateTime(_viewDate.Year, _viewDate.Month, 1);

        // En France, la semaine commence le lundi (DayOfWeek.Monday = 1, Sunday = 0)
        int offset = ((int)firstDayOfMonth.DayOfWeek + 6) % 7;

        for (int i = 0; i < offset; i++) days.Add(null);

        int daysInMonth = DateTime.DaysInMonth(_viewDate.Year, _viewDate.Month);
        for (int i = 1; i <= daysInMonth; i++)
        {
            days.Add(new DateTime(_viewDate.Year, _viewDate.Month, i));
        }

        _cachedDays = days;
        _lastRenderedMonth = _viewDate;
        return _cachedDays;
    }

    private bool IsDisabled(DateTime date)
    {
        if (Min.HasValue && date.Date < Min.Value.Date) return true;
        if (Max.HasValue && date.Date > Max.Value.Date) return true;
        return false;
    }

    private bool IsSelected(DateTime date) => Value.HasValue && Value.Value.Date == date.Date;
    private static bool IsToday(DateTime date) => date.Date == DateTime.Today;
}