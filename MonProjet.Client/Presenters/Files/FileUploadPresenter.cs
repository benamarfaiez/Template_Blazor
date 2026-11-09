using Microsoft.AspNetCore.Components.Forms;
using MonProjet.Client.Models.UI.Files;

namespace MonProjet.Client.Presenters.Files;

public class FileUploadPresenter : IFileUploadPresenter
{
    public List<ClientFileInfo> Files { get; } = [];
    public FileUploadConfig Config { get; set; } = new();
    public event Action? OnStateChanged;
    public event Action<string>? OnErrorOccurred;

    public Task AddFilesAsync(IReadOnlyList<IBrowserFile> files)
    {
        if (files == null || files.Count == 0) return Task.CompletedTask;

        if (!Config.AllowMultiple)
        {
            Files.Clear();
            files = [files[0]]; // On ne garde que le premier
        }
        else
        {
            // Vérification de la capacité totale
            int remainingCapacity = Config.MaxFileCount - Files.Count;

            if (remainingCapacity <= 0)
            {
                OnErrorOccurred?.Invoke($"Vous avez atteint la limite de {Config.MaxFileCount} fichiers.");
                return Task.CompletedTask;
            }

            if (files.Count > remainingCapacity)
            {
                files = [.. files.Take(remainingCapacity)];
            }
        }

        foreach (var file in files)
        {
            var clientFile = new ClientFileInfo(file);

            // Validation de la taille
            if (file.Size > Config.MaxFileSizeInBytes)
            {
                clientFile.Status = FileUploadStatus.Error;
                clientFile.ErrorMessage = $"Fichier trop lourd (Max: {Config.MaxFileSizeFormatted})";
            }
            // Validation de l'extension
            else if (Config.AllowedExtensions.Length > 0)
            {
                var ext = Path.GetExtension(file.Name).ToLowerInvariant();
                if (!Config.AllowedExtensions.Contains(ext))
                {
                    clientFile.Status = FileUploadStatus.Error;
                    clientFile.ErrorMessage = $"Le type de fichier '{ext}' n'est pas autorisé.";
                }
            }

            Files.Add(clientFile);
        }
        OnStateChanged?.Invoke();
        return Task.CompletedTask;
    }
    public void RemoveFile(ClientFileInfo file)
    {
        Files.Remove(file);
        OnStateChanged?.Invoke();
    }
    public void RemoveFiles(IEnumerable<ClientFileInfo> files)
    {
        if (files == null) return;

        foreach (var file in files)
        {
            Files.Remove(file);
        }
        OnStateChanged?.Invoke();
    }

    public void Clear()
    {
        Files.Clear();
        OnStateChanged?.Invoke();
    }
}
