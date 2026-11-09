using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using MonProjet.Client.Components.UI.Core;
using MonProjet.Client.Helpers;
using MonProjet.Client.Models.UI.Files;

namespace MonProjet.Client.Components.UI.BasicInput;

public partial class AppFileUpload : AppComponentBase, IAsyncDisposable
{
    [Parameter] public FileUploadConfig Config { get; set; } = new();
    [Parameter] public List<ClientFileInfo> Files { get; set; } = [];
    [Parameter] public EventCallback<IReadOnlyList<IBrowserFile>> OnFilesSelected { get; set; }
    [Parameter] public EventCallback<ClientFileInfo> OnRemoveFile { get; set; }

    private bool _isDragging;
    private ElementReference _containerRef;
    private InputFile _inputFileRef = default!;
    private IJSObjectReference? _jsModule;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            try
            {
                _jsModule = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./js/file-drop.js");
                await _jsModule.InvokeVoidAsync("initDropZone", _containerRef, _inputFileRef.Element);
            }
            catch (JSDisconnectedException)
            {
            }
            catch (Exception)
            {
                //Logger.LogError(ex, "Erreur lors de l'initialisation du module JS pour le Drag & Drop.");
            }
        }
    }

    private void HandleDragEnter() => _isDragging = true;
    private void HandleDragLeave() => _isDragging = false;

    private async Task OnInputFileChange(InputFileChangeEventArgs e)
    {
        _isDragging = false;
        if (OnFilesSelected.HasDelegate)
        {
            await OnFilesSelected.InvokeAsync(e.GetMultipleFiles());
        }
    }

    private static string GetIcon(string fileName) => FileIconHelper.GetIcon(fileName);

    public async ValueTask DisposeAsync()
    {
        if (_jsModule is not null)
        {
            try
            {
                await _jsModule.DisposeAsync();
            }
            catch (JSDisconnectedException)
            {
            }
            catch (Exception)
            {
                // Logger.LogWarning(ex, "Erreur mineure lors de la libération du module JS.");
            }
        }
        GC.SuppressFinalize(this);
    }
}