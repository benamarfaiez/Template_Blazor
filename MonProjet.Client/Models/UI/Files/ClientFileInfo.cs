using Microsoft.AspNetCore.Components.Forms;
using MonProjet.Client.Helpers;

namespace MonProjet.Client.Models.UI.Files;

public class ClientFileInfo(IBrowserFile browserFile)
{
    public Guid Id { get; } = Guid.NewGuid();

    /// <summary>
    /// Référence au fichier natif du navigateur.
    /// Nécessaire pour lire le flux (Stream) lors de l'upload.
    /// </summary>
    public IBrowserFile BrowserFile { get; } = browserFile ?? throw new ArgumentNullException(nameof(browserFile));

    public string Name => BrowserFile.Name;
    public long SizeInBytes => BrowserFile.Size;
    public string ContentType => BrowserFile.ContentType;

    public FileUploadStatus Status { get; set; } = FileUploadStatus.Pending;
    public string? ErrorMessage { get; set; }

    // ✅ Le modèle utilise le Helper (pas de logique interne)
    public string FormattedSize => FileSizeHelper.Format(SizeInBytes);
}