export function setLanguage(culture) {
    localStorage.setItem('culture', culture);

    // ✅ Création du cookie pour que le Serveur (SSR) puisse lire la langue
    document.cookie = `.AspNetCore.Culture=c=${culture}|uic=${culture}; path=/; max-age=31536000; SameSite=Lax`;
}

export function getLanguage() {
    return localStorage.getItem('culture');
}