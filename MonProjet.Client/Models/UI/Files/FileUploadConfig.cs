using MonProjet.Client.Helpers;

namespace MonProjet.Client.Models.UI.Files;

public class FileUploadConfig
{
    public long MaxFileSizeInBytes { get; set; } = 10 * 1024 * 1024; // 10 Mo
    public string[] AllowedExtensions { get; set; } = [];
    public bool AllowMultiple { get; set; } = true;

    // Helper pratique pour l'affichage UI
    public string MaxFileSizeFormatted => FileSizeHelper.Format(MaxFileSizeInBytes);
}