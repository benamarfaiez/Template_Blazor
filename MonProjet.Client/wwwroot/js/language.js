export function setLanguage(culture) {
    try {
        localStorage.setItem('culture', culture);
    } catch {
    }

    try {
        const requestCulture = encodeURIComponent(`c=${culture}|uic=${culture}`);
        document.cookie = `.AspNetCore.Culture=${requestCulture}; path=/; max-age=31536000; samesite=lax`;
    } catch {
    }
}

export function getLanguage() {
    try {
        return localStorage.getItem('culture');
    } catch {
        return null;
    }
}