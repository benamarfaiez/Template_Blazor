using Bunit;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.DependencyInjection;
using MonProjet.Client.Components.Pages.Gallery;
using MonProjet.Client.Components.UI.BasicInput;
using MonProjet.Client.Models.Pages.Gallery;
using MonProjet.Client.Models.UI.Files;
using MonProjet.Client.Presenters.Files;
using MonProjet.Client.Presenters.Gallery;
using MonProjet.Client.Services;
using NSubstitute;
using Xunit;

namespace MonProjet.Client.Tests.Components.Pages.Gallery;

public sealed class GalleryViewTests
{
    [Fact]
    public void Render_RegistersViewInitializesPresenterAndDisplaysViewModelItems()
    {
        using var context = new BunitContext();
        ConfigureGalleryJavaScript(context);
        var viewModel = new GalleryViewModel
        {
            Items = [new(1, "Test A", "Actif", 10), new(2, "Test B", "Inactif", 20)],
            Suggestions = ["Blazor", "WASM"]
        };
        var presenter = CreatePresenter(viewModel);
        context.Services.AddScoped<IGalleryPresenter>(_ => presenter);
        context.Services.AddSingleton(Substitute.For<IToastService>());

        var cut = context.Render<GalleryView>();

        presenter.Received(1).SetView(Arg.Any<IGalleryView>());
        presenter.Received(1).InitializeAsync();
        Assert.Equal(2, cut.FindAll(".app-datagrid tbody tr").Count);
        Assert.Contains("Test A", cut.Markup);
        Assert.Contains("WASM", cut.Markup);
    }

    [Fact]
    public async Task FileSelection_ForwardsFilesToUploaderPresenter()
    {
        using var context = new BunitContext();
        ConfigureGalleryJavaScript(context);
        var presenter = CreatePresenter(new GalleryViewModel());
        context.Services.AddScoped<IGalleryPresenter>(_ => presenter);
        context.Services.AddSingleton(Substitute.For<IToastService>());
        var cut = context.Render<GalleryView>();
        var file = Substitute.For<IBrowserFile>();

        await cut.InvokeAsync(() => cut.FindComponent<AppFileUpload>().Instance.OnFilesSelected.InvokeAsync([file]));

        await presenter.FileUploader.Received(1).AddFilesAsync(Arg.Is<IReadOnlyList<IBrowserFile>>(files => files.Count == 1 && ReferenceEquals(files[0], file)));
    }

    private static IGalleryPresenter CreatePresenter(GalleryViewModel viewModel)
    {
        var uploader = Substitute.For<IFileUploadPresenter>();
        uploader.Config.Returns(new FileUploadConfig());
        uploader.Files.Returns([]);
        var presenter = Substitute.For<IGalleryPresenter>();
        presenter.ViewModel.Returns(viewModel);
        presenter.FileUploader.Returns(uploader);
        presenter.InitializeAsync().Returns(Task.CompletedTask);
        presenter.UploadAllFilesAsync().Returns(Task.CompletedTask);
        uploader.AddFilesAsync(Arg.Any<IReadOnlyList<IBrowserFile>>()).Returns(Task.CompletedTask);
        return presenter;
    }

    private static void ConfigureGalleryJavaScript(BunitContext context)
    {
        context.JSInterop.SetupModule("./js/file-drop.js").SetupVoid("initDropZone");
        context.JSInterop.SetupModule("./js/dropdown.js").SetupVoid("registerFocusOut", _ => true);
        context.JSInterop.SetupModule("./Components/UI/Text/MonacoEditor.razor.js").SetupVoid("init", _ => true);
    }
}