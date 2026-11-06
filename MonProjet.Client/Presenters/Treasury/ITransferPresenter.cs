using MonProjet.Client.Models.Pages.Treasury;
using MonProjet.Client.Presenters.Common;
using MonProjet.Client.View.Treasury;

namespace MonProjet.Client.Presenters.Treasury;

public interface ITransferPresenter : IPresenter<ITransferView>
{
    TransferViewModel ViewModel { get; }
    Task InitializeAsync();
    Task ExecuteTransferAsync();
    Task<bool> ValidateStepAsync(int currentIndex, int nextIndex);
}