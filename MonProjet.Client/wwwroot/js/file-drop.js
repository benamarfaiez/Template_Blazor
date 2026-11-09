export function initDropZone(containerElement, inputElement) {
    // Empêche le navigateur d'ouvrir le fichier
    containerElement.addEventListener('dragover', (e) => {
        e.preventDefault();
        e.stopPropagation();
    });

    // Gère le drop
    containerElement.addEventListener('drop', (e) => {
        e.preventDefault();
        e.stopPropagation();

        if (e.dataTransfer.files && e.dataTransfer.files.length > 0) {
            // L'astuce : on assigne les fichiers droppés à l'input natif
            inputElement.files = e.dataTransfer.files;

            // On déclenche l'événement 'change' pour que Blazor le capte
            inputElement.dispatchEvent(new Event('change', { bubbles: true }));
        }
    });
}