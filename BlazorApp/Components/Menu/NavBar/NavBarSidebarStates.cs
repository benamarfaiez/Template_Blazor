namespace BlazorApp.Components.Menu.NavBar;

[Flags]
internal enum NavBarSidebarStates
{
    Hidden = 0,
    Collapsed = 2,
    Expanded = 4,
    ExpandedOverlay = 8
}
