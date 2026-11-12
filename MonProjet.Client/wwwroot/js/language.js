export function setLanguage(culture) {
    localStorage.setItem('culture', culture);
}

export function getLanguage() {
    return localStorage.getItem('culture');
}