using Microsoft.AspNetCore.Components;

namespace MonProjet.Client.Services;

public interface IFocusTrapService : IAsyncDisposable
{
    Task TrapAsync(ElementReference element);
    Task ReleaseAsync(ElementReference element);
}