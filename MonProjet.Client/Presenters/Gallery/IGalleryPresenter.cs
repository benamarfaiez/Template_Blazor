using MonProjet.Client.Models.Gallery;
using MonProjet.Client.Presenters.Common;

namespace MonProjet.Client.Presenters.Gallery;

public interface IGalleryPresenter : IPresenter<IGalleryView>
{
    GalleryViewModel ViewModel { get; }
    Task InitializeAsync();
}

public interface IGalleryView : IView { }