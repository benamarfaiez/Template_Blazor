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
    [Parameter] public List<NavLinkItem> Links { get; set; } = [];
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
            var newActiveLink = Links.FirstOrDefault(l => l.Href == _activeDropdown.Href);

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
        CloseAll();
    }

    private void ToggleDropdown(NavLinkItem link)
    {
        if (!link.HasDropdown) return;
        _activeDropdown = _activeDropdown == link ? null : link;
    }

    private async Task OnMouseEnterAsync(NavLinkItem link)
    {
        if (link is null) return;

        _hoverCts?.Cancel();

        if (link.HasDropdown)
        {
            if (_activeDropdown != link)
            {
                _activeDropdown = link;
                StateHasChanged();
            }
        }
        else
        {
            if (_activeDropdown is not null)
            {
                _activeDropdown = null;
                StateHasChanged();
            }
        }
    }

    private async Task OnMouseLeaveAsync()
    {
        _hoverCts?.Cancel();
        _hoverCts?.Dispose();
        _hoverCts = new CancellationTokenSource();

        try
        {
            await Task.Delay(200, _hoverCts.Token);
            if (_hoverCts.IsCancellationRequested) return;
            _activeDropdown = null;
            StateHasChanged();
        }
        catch (TaskCanceledException)
        {
        }
        catch (ObjectDisposedException)
        {
        }
    }

    private void CloseAll()
    {
        _activeDropdown = null;
        _isMobileMenuOpen = false;
        StateHasChanged();
    }

    private void ToggleMobileMenu()
    {
        _isMobileMenuOpen = !_isMobileMenuOpen;
        _activeDropdown = null;
    }

    public void Dispose()
    {
        NavManager.LocationChanged -= HandleLocationChanged;
        if (_hoverCts is not null)
        {
            _hoverCts.Cancel();
            _hoverCts.Dispose();
            _hoverCts = null;
        }
        GC.SuppressFinalize(this);
    }
}