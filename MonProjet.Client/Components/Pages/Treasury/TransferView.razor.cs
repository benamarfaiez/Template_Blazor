using Microsoft.AspNetCore.Components;
using MonProjet.Client.Components.UI.Core;
using MonProjet.Client.Presenters.Treasury;
using MonProjet.Client.Services;
using MonProjet.Client.View.Treasury;

namespace MonProjet.Client.Components.Pages.Treasury;

public partial class TransferView : OwningComponentBase, ITransferView, IDisposable
{
    private ITransferPresenter Presenter => ScopedServices.GetRequiredService<ITransferPresenter>();

    [Inject] private NavigationManager NavManager { get; set; } = default!;
    [Inject] private IToastService ToastService { get; set; } = default!;

    private int _currentStep = 0;

    protected override async Task OnInitializedAsync()
    {
        Presenter.SetView(this);
        await Presenter.InitializeAsync();
    }

    // ✅ Délégation de la validation au présentateur
    private Task<bool> ValidateStepAsync(int currentIndex, int nextIndex)
    {
        return Presenter.ValidateStepAsync(currentIndex, nextIndex);
    }

    // ✅ Implémentation de ITransferView (Appelée par le Présentateur)
    public void OnTransferCompleted()
    {
        NavManager.NavigateTo("/");
    }

    // ✅ Implémentation de IView
    public void ShowError(string message)
    {
        ToastService.ShowError(message);
        StateHasChanged();
    }

    public void ShowLoading(bool isLoading)
    {
        StateHasChanged();
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}