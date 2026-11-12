export function registerFocusOut(element, dotNetReference) {
    element.addEventListener('focusout', event => {
        if (!element.contains(event.relatedTarget)) {
            void dotNetReference.invokeMethodAsync('HandleFocusOutAsync');
        }
    });
}
