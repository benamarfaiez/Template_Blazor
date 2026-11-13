using Microsoft.AspNetCore.Components.Forms;
using MonProjet.Client.Models.UI.Files;
using MonProjet.Client.Presenters.Files;
using MonProjet.Client.Presenters.Gallery;
using MonProjet.Client.Services;
using NSubstitute;
using Xunit;

namespace MonProjet.Client.Tests.Presenters.Gallery;

[Trait("Category", "Unit")]
public sealed class GalleryPresenterTests
{
    [Fact]
    public void Constructor_ConfiguresFileUploader()
    {
        var uploader = Substitute.For<IFileUploadPresenter>();
        var presenter = new GalleryPresenter(
            Substitute.For<IExceptionHandler>(),
            Substitute.For<IToastService>(),
            uploader);

        Assert.Same(uploader, presenter.FileUploader);
        Assert.Equal(5 * 1024 * 1024, uploader.Config.MaxFileSizeInBytes);
        Assert.Equal([".pdf", ".jpg", ".png"], uploader.Config.AllowedExtensions);
        Assert.True(uploader.Config.AllowMultiple);
        Assert.Equal(5, uploader.Config.MaxFileCount);
    }

    [Fact]
    public async Task UploadAllFilesAsync_UploadsValidFilesAndNotifiesView()
    {
        var file = new ClientFileInfo(Substitute.For<IBrowserFile>());
        var uploader = Substitute.For<IFileUploadPresenter>();
        uploader.Files.Returns([file]);
        var toastService = Substitute.For<IToastService>();
        var presenter = new GalleryPresenter(
            Substitute.For<IExceptionHandler>(),
            toastService,
            uploader);

        await presenter.UploadAllFilesAsync();

        Assert.False(presenter.ViewModel.IsUploading);
        toastService.Received(1).ShowSuccess("1 fichier(s) uploadé(s).");
        uploader.Received(1).RemoveFiles(Arg.Is<IEnumerable<ClientFileInfo>>(files => files.Count() == 1));
    }
}
