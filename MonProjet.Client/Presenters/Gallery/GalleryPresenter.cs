using MonProjet.Client.Models.Gallery;

namespace MonProjet.Client.Presenters.Gallery;

public class GalleryPresenter : IGalleryPresenter
{
    private IGalleryView? _view;
    public GalleryViewModel ViewModel { get; } = new();

    public void SetView(IGalleryView view) => _view = view;

    public Task InitializeAsync()
    {
        // Logique d'initialisation si nécessaire
        return Task.CompletedTask;
    }
}