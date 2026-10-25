const traps = new WeakMap();

export function trap(element) {
    if (!element) return;
    const previous = document.activeElement;
    const selector = 'a[href], button:not([disabled]), textarea:not([disabled]), input:not([disabled]), select:not([disabled]), [tabindex]:not([tabindex="-1"])';
    const handler = (e) => {
        if (e.key !== 'Tab') return;
        const focusable = Array.from(element.querySelectorAll(selector));
        if (focusable.length === 0) return;
        const first = focusable[0];
        const last = focusable[focusable.length - 1];
        if (e.shiftKey && document.activeElement === first) { e.preventDefault(); last.focus(); }
        else if (!e.shiftKey && document.activeElement === last) { e.preventDefault(); first.focus(); }
    };
    document.addEventListener('keydown', handler);
    traps.set(element, { handler, previous });
    (element.querySelector(selector))?.focus();
}

export function release(element) {
    const state = traps.get(element);
    if (!state) return;
    document.removeEventListener('keydown', state.handler);
    state.previous?.focus?.();
    traps.delete(element);
}