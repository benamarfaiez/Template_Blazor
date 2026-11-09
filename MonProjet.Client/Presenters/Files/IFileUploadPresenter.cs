using Microsoft.AspNetCore.Components.Forms;
using MonProjet.Client.Models.UI.Files;

namespace MonProjet.Client.Presenters.Files;

public interface IFileUploadPresenter
{
    List<ClientFileInfo> Files { get; }
    FileUploadConfig Config { get; set; }
    event Action? OnStateChanged;

    Task AddFilesAsync(IReadOnlyList<IBrowserFile> files);
    void RemoveFile(ClientFileInfo file);
    void Clear();
}
