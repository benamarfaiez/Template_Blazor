using MonProjet.Client.Models.Pages.Gallery;
using MonProjet.Client.Presenters.Common;

namespace MonProjet.Client.Presenters.Gallery;

public interface IGalleryPresenter : IPresenter<IGalleryView>
{
    GalleryViewModel ViewModel { get; }
    Task InitializeAsync();
}

public interface IGalleryView : IView { }