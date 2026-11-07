export function setTheme(isDark) {
    const theme = isDark ? 'dark' : 'light';
    document.documentElement.dataset.theme = theme;

    try {
        localStorage.setItem('theme', theme);
    } catch {
    }
}

export function getTheme() {
    let savedTheme;

    try {
        savedTheme = localStorage.getItem('theme');
    } catch {
    }

    const isDark = savedTheme
        ? savedTheme === 'dark'
        : window.matchMedia?.('(prefers-color-scheme: dark)').matches ?? false;

    document.documentElement.dataset.theme = isDark ? 'dark' : 'light';
    return isDark;
}