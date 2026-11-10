using Microsoft.AspNetCore.Components;
using MonProjet.Client.Components.UI.Core;
using MonProjet.Client.Models.UI.Date;

namespace MonProjet.Client.Components.UI.BasicInput;

public partial class AppDateRangePicker : AppComponentBase, IDisposable
{
    private readonly DateRangePickerViewModel _viewModel = new();

    // La liste des presets disponibles
    private readonly List<DateRangePreset> _presets = [.. Enum.GetValues<DateRangePreset>()];

    [Parameter] public DateRange? InitialRange { get; set; }
    [Parameter] public EventCallback<DateRange> OnRangeChanged { get; set; }

    protected override void OnParametersSet()
    {
        if (InitialRange is not null)
        {
            if (InitialRange.Start != _viewModel.CurrentRange.Start || InitialRange.End != _viewModel.CurrentRange.End)
            {
                _viewModel.SetDates(InitialRange.Start, InitialRange.End);
            }
        }
    }

    protected override void OnInitialized()
    {
        _viewModel.OnStateChanged += HandleStateChanged;
    }

    private void HandleStateChanged()
    {
        _ = InvokeAsync(async () =>
        {
            StateHasChanged();
            await NotifyParentAsync();
        });
    }

    private async Task NotifyParentAsync()
    {
        if (OnRangeChanged.HasDelegate && string.IsNullOrEmpty(_viewModel.ErrorMessage))
        {
            try
            {
                await OnRangeChanged.InvokeAsync(_viewModel.CurrentRange);
            }
            catch (Exception)
            {
            }
        }
    }

    private void HandlePresetChanged(DateRangePreset preset)
    {
        _viewModel.ApplyPreset(preset);
    }

    private void OnStartDateChanged(DateTime? newDate)
    {
        _viewModel.SetDates(newDate, _viewModel.CurrentRange.End);
    }

    private void OnEndDateChanged(DateTime? newDate)
    {
        _viewModel.SetDates(_viewModel.CurrentRange.Start, newDate);
    }

    private static string GetPresetLabel(DateRangePreset preset) => preset switch
    {
        DateRangePreset.Today => "Aujourd'hui",
        DateRangePreset.Last7Days => "7 derniers jours",
        DateRangePreset.Last30Days => "30 derniers jours",
        DateRangePreset.ThisMonth => "Ce mois-ci",
        DateRangePreset.LastMonth => "Mois dernier",
        DateRangePreset.ThisYear => "Cette année",
        DateRangePreset.Custom => "Choisir un raccourci...",
        _ => "Personnalisé"
    };
    public void Dispose()
    {
        _viewModel.OnStateChanged -= HandleStateChanged;
        GC.SuppressFinalize(this);
    }
}