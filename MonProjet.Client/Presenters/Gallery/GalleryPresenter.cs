using MonProjet.Client.Models.Pages.Gallery;
using MonProjet.Client.Models.UI.Files;
using MonProjet.Client.Presenters.Files;
using MonProjet.Client.Services;

namespace MonProjet.Client.Presenters.Gallery;

public class GalleryPresenter : IGalleryPresenter
{
    private IGalleryView? _view;
    private readonly IExceptionHandler _exceptions;
    private readonly IToastService _toastService;

    public GalleryViewModel ViewModel { get; } = new();

    public IFileUploadPresenter FileUploader { get; }

    public GalleryPresenter(
        IExceptionHandler exceptions,
        IToastService toastService,
        IFileUploadPresenter fileUploader)
    {
        _exceptions = exceptions;
        _toastService = toastService;
        FileUploader = fileUploader;

        FileUploader.Config = new()
        {
            MaxFileSizeInBytes = 5 * 1024 * 1024, // 5 Mo
            AllowedExtensions = [".pdf", ".jpg", ".png", ".docx"],
            AllowMultiple = true
        };

        FileUploader.OnStateChanged += () => _view?.ShowLoading(false);
    }

    public void SetView(IGalleryView view) => _view = view;

    public Task InitializeAsync()
    {
        return Task.CompletedTask;
    }

    public async Task UploadAllFilesAsync()
    {
        var validFiles = FileUploader.Files.Where(f => f.Status != FileUploadStatus.Error).ToList();
        if (validFiles.Count == 0) return;

        ViewModel.IsUploading = true;
        _view?.ShowLoading(true);

        try
        {
            foreach (var file in validFiles)
            {
                await Task.Delay(500); // Simulation API
                Console.WriteLine($"Upload de : {file.Name}");
            }

            _toastService.ShowSuccess($"{validFiles.Count} fichier(s) uploadé(s).");
            FileUploader.Clear();
        }
        catch (Exception ex)
        {
            await _exceptions.HandleAsync(ex, "Erreur upload.");
        }
        finally
        {
            ViewModel.IsUploading = false;
            _view?.ShowLoading(false);
        }
    }
}