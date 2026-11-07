export function setTheme(isDark) {
    if (isDark) {
        document.documentElement.classList.add('dark-mode');
        localStorage.setItem('theme', 'dark');
    } else {
        document.documentElement.classList.remove('dark-mode');
        localStorage.setItem('theme', 'light');
    }
}

export function getTheme() {
    const savedTheme = localStorage.getItem('theme');
    if (savedTheme) {
        return savedTheme === 'dark';
    }
    // Si aucun thème sauvegardé, on utilise les préférences du système
    return window.matchMedia && window.matchMedia('(prefers-color-scheme: dark)').matches;
}