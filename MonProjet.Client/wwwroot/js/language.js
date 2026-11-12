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
    let savedCulture = null;
    try {
        savedCulture = localStorage.getItem('culture');
    } catch {
    }

    if (savedCulture) {
        return savedCulture;
    }

    try {
        const cultureCookie = document.cookie
            .split(';')
            .map(cookie => cookie.trim())
            .find(cookie => cookie.startsWith('.AspNetCore.Culture='));
        if (!cultureCookie) {
            return null;
        }

        const cookieValue = decodeURIComponent(cultureCookie.substring('.AspNetCore.Culture='.length));
        return cookieValue.split('|').find(value => value.startsWith('c='))?.substring(2) ?? null;
    } catch {
        return null;
    }
}