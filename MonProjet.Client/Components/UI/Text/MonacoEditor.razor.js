const instances = new Map();
let monacoLoading;

function loadMonaco() {
    if (monacoLoading) return monacoLoading;
    monacoLoading = new Promise((resolve) => {
        if (window.monaco) return resolve(window.monaco);
        const script = document.createElement('script');
        script.src = 'https://cdn.jsdelivr.net/npm/monaco-editor@0.52.0/min/vs/loader.js';
        script.onload = () => {
            window.require.config({ paths: { vs: 'https://cdn.jsdelivr.net/npm/monaco-editor@0.52.0/min/vs' } });
            window.require(['vs/editor/editor.main'], () => resolve(window.monaco));
        };
        document.head.appendChild(script);
    });
    return monacoLoading;
}

export async function init(container, dotNetRef, initialValue, language, height, options) {
    const monaco = await loadMonaco();
    container.style.height = height;

    const editor = monaco.editor.create(container, {
        value: initialValue,
        language: language,
        theme: options.theme,
        readOnly: options.readOnly,
        wordWrap: options.wordWrap ? 'on' : 'off',
        minimap: { enabled: options.minimapEnabled },
        lineNumbers: options.lineNumbersVisible ? 'on' : 'off',
        formatOnPaste: options.autoFormat,
        formatOnType: options.autoFormat,
        automaticLayout: true
    });

    editor.onDidChangeModelContent(() => {
        dotNetRef.invokeMethodAsync('OnEditorChanged', editor.getValue());
    });

    instances.set(container, editor);
}

export function setValue(container, value) {
    const editor = instances.get(container);
    if (editor && editor.getValue() !== value) {
        editor.setValue(value);
    }
}

export function dispose(container) {
    const editor = instances.get(container);
    if (editor) {
        editor.dispose();
        instances.delete(container);
    }
}