export function getContextMenuDimensions(menuElement) {
    if (!menuElement) return { WindowWidth: 0, WindowHeight: 0, MenuWidth: 0, MenuHeight: 0 };

    const rect = menuElement.getBoundingClientRect();

    return {
        WindowWidth: window.innerWidth,
        WindowHeight: window.innerHeight,
        MenuWidth: rect.width,
        MenuHeight: rect.height
    };
}