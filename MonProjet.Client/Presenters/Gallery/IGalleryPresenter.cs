using Microsoft.AspNetCore.Components.Forms;
using MonProjet.Client.Models.Pages.Gallery;
using MonProjet.Client.Models.UI.Files;
using MonProjet.Client.Presenters.Common;
using MonProjet.Client.Presenters.Files;

namespace MonProjet.Client.Presenters.Gallery;

public interface IGalleryPresenter : IPresenter<IGalleryView>
{
    GalleryViewModel ViewModel { get; }
    Task InitializeAsync();
    IFileUploadPresenter FileUploader { get; }
    Task UploadAllFilesAsync();
}

public interface IGalleryView : IView { }