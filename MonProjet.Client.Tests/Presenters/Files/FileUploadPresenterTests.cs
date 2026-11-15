using Microsoft.AspNetCore.Components.Forms;
using MonProjet.Client.Models.UI.Files;
using MonProjet.Client.Presenters.Files;
using NSubstitute;
using Xunit;

namespace MonProjet.Client.Tests.Presenters.Files;

[Trait("Category", "Unit")]
public sealed class FileUploadPresenterTests
{
    [Fact]
    public async Task AddFilesAsync_MarksUnsupportedExtensionAsError()
    {
        var presenter = new FileUploadPresenter
        {
            Config = new FileUploadConfig
            {
                AllowedExtensions = [".pdf"],
                MaxFileSizeInBytes = 1024
            }
        };

        await presenter.AddFilesAsync([CreateFile("image.png", 100)]);

        var file = Assert.Single(presenter.Files);
        Assert.Equal(FileUploadStatus.Error, file.Status);
        Assert.Contains(".png", file.ErrorMessage);
    }

    [Fact]
    public async Task AddFilesAsync_LimitsFilesToRemainingCapacity()
    {
        var presenter = new FileUploadPresenter
        {
            Config = new FileUploadConfig { MaxFileCount = 1 }
        };

        await presenter.AddFilesAsync([CreateFile("first.pdf", 100), CreateFile("second.pdf", 100)]);

        var file = Assert.Single(presenter.Files);
        Assert.Equal("first.pdf", file.Name);
    }

    [Fact]
    public async Task AddFilesAsync_AllowMultipleFalseReplacesExistingFiles()
    {
        var presenter = new FileUploadPresenter
        {
            Config = new FileUploadConfig { AllowMultiple = false }
        };
        await presenter.AddFilesAsync([CreateFile("old.pdf", 100)]);

        await presenter.AddFilesAsync([CreateFile("new.pdf", 100), CreateFile("ignored.pdf", 100)]);

        var file = Assert.Single(presenter.Files);
        Assert.Equal("new.pdf", file.Name);
    }

    private static IBrowserFile CreateFile(string name, long size)
    {
        var file = Substitute.For<IBrowserFile>();
        file.Name.Returns(name);
        file.Size.Returns(size);
        file.ContentType.Returns("application/pdf");
        return file;
    }
}
