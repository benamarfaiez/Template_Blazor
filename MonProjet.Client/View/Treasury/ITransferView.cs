using Microsoft.AspNetCore.Components;
using MonProjet.Client.Presenters.Common;

namespace MonProjet.Client.View.Treasury;

public interface ITransferView : IView {
    void OnTransferCompleted();
}