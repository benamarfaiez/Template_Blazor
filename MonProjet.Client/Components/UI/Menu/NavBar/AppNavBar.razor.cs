using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using MonProjet.Client.Components.UI.Core;
using MonProjet.Client.Models.UI.Menu.NavBar;

namespace MonProjet.Client.Components.UI.Menu.NavBar;
public partial class AppNavBar : AppComponentBase, IDisposable
{
    [Inject] private NavigationManager NavManager { get; set; } = default!;

    [Parameter] public string Brand { get; set; } = "MonProjet";
    [Parameter] public RenderFragment? BrandTemplate { get; set; }
    [Parameter] public IReadOnlyList<NavLinkItem> Links { get; set; } = [];
    [Parameter] public RenderFragment? RightContent { get; set; }

    private NavLinkItem? _activeDropdown;
    private bool _isMobileMenuOpen;
    private CancellationTokenSource? _hoverCts;

    protected override void OnInitialized()
    {
        NavManager.LocationChanged += HandleLocationChanged;
    }

    protected override void OnParametersSet()
    {
        if (_activeDropdown is not null)
        {
            var newActiveLink = Links.FirstOrDefault(l => l.Id == _activeDropdown.Id);

            if (newActiveLink is not null)
            {
                _activeDropdown = newActiveLink;
            }
            else
            {
                CloseAll();
            }
        }
    }
    private void HandleLocationChanged(object? sender, LocationChangedEventArgs e)
    {
        _ = InvokeAsync(() =>
        {
            CloseAll();
            StateHasChanged();
        });
    }

    private void ToggleDropdown(NavLinkItem link)
    {
        if (!link.HasDropdown) return;
        _activeDropdown = _activeDropdown?.Id == link.Id ? null : link;
    }

    private Task OnMouseEnterAsync(NavLinkItem link)
    {
        CancelPendingClose();
        _activeDropdown = link.HasDropdown ? link : null;
        return Task.CompletedTask;
    }

    private async Task OnMouseLeaveAsync()
    {
        CancelPendingClose();
        var hoverCts = _hoverCts = new CancellationTokenSource();

        try
        {
            await Task.Delay(200, hoverCts.Token);
            if (ReferenceEquals(_hoverCts, hoverCts))
            {
                _hoverCts = null;
                _activeDropdown = null;
            }
        }
        catch (OperationCanceledException)
        {
        }
        finally
        {
            if (ReferenceEquals(_hoverCts, hoverCts))
            {
                _hoverCts = null;
            }

            hoverCts.Dispose();
        }
    }

    private void CloseAll()
    {
        CancelPendingClose();
        _activeDropdown = null;
        _isMobileMenuOpen = false;
    }

    private void ToggleMobileMenu()
    {
        _isMobileMenuOpen = !_isMobileMenuOpen;
        _activeDropdown = null;
        CancelPendingClose();
    }

    private void CancelPendingClose()
    {
        _hoverCts?.Cancel();
        _hoverCts?.Dispose();
        _hoverCts = null;
    }

    public void Dispose()
    {
        NavManager.LocationChanged -= HandleLocationChanged;
        CancelPendingClose();
        GC.SuppressFinalize(this);
    }
}