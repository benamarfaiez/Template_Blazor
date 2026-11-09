using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MonProjet.Client.Components.UI.Collections.Column;
using MonProjet.Client.Components.UI.Helpers;
using MonProjet.Client.Models.UI.Files;
using MonProjet.Client.Models.UI.Menu;
using MonProjet.Client.Presenters.Gallery;
using MonProjet.Client.Services;
using MonProjet.Shared.Models.Gallery;

namespace MonProjet.Client.Components.Pages.Gallery;

public partial class GalleryView
{
    [Inject] private IToastService ToastService { get; set; } = default!;
    // 1. Résolution du présentateur
    private IGalleryPresenter Presenter => ScopedServices.GetRequiredService<IGalleryPresenter>();

    // 2. États pour les composants interactifs
    private bool _isChecked = true;
    private bool _isToggled = false;
    private string _radioValue = "1";
    private string? _selectedCombo;
    private string _textValue = "";
    private string _recherche = "";
    private string _email = "";
    private string? _suggestValue;
    private string _codeValue = "public class Hello { }";
    private bool _isEditorOpen = false;
    // 3. États pour les fenêtres modales
    private bool _showDialog = false;
    private bool _showOverlay = false;
    private bool _showFullScreen = false;

    // 4. Configuration du ContextMenu
    private List<ContextMenuItem> _contextMenuItems = [];

    private void InitializeContextMenu()
    {
        _contextMenuItems =
        [
            new() {
                Label = "Modifier",
                Icon = "✏️",
                OnClick = EventCallback.Factory.Create(this, EditAction)
            },
            new() {
                Label = "Dupliquer",
                Icon = "📄",
                OnClick = EventCallback.Factory.Create(this, DuplicateAction)
            },
            ContextMenuItem.Separator(),
            new() {
                Label = "Archiver",
                Icon = "📦",
                OnClick = EventCallback.Factory.Create(this, ArchiveActionAsync)
            },
            new() {
                Label = "Supprimer",
                Icon = "🗑️",
                IsDisabled = true,
                OnClick = EventCallback.Factory.Create(this, DeleteAction)
            }
        ];
    }

    private void EditAction()
    {
        Console.WriteLine("Modifié");
    }

    private static bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email)) return false;

        // Validation simple (vous pouvez utiliser une Regex plus complexe)
        return email.Contains('@') && email.Contains('.') && email.Length >= 5;
    }
    private void DuplicateAction() => Console.WriteLine("Dupliqué");
    private async Task ArchiveActionAsync()
    {
        Console.WriteLine("Archivé");
    }
    private void DeleteAction() => Console.WriteLine("Supprimé");

    private int _quantite = 1;
    private decimal _prix = 0m;
    private int _volume = 50;
    private readonly List<string> _allTags = ["C#", "Blazor", ".NET", "WASM", "SOLID"];
    private IEnumerable<string> _selectedTags = ["Blazor"];
    private DateTime? _dateNaissance;
    private string _selectedLanguage = "";
    private string? _demoTab;

    private static void EditProject() { /* Logique */ }
    private static void DuplicateProject() { /* Logique */ }
    private static void DeleteProject() { /* Logique */ }

    private void DeleteEmployee()
    {
        // Logique de suppression...
        _showDialog = false; // Ferme la modale
    }
    // 5. Configuration du DataGrid
    private readonly List<GridColumn<DemoItemDto>> _dataGridColumns;

    public GalleryView()
    {
        _dataGridColumns =
        [
            GridColumn.Text<DemoItemDto>("ID", i => i.Id, comparer: (a,b) => a.Id.CompareTo(b.Id)),
            GridColumn.Text<DemoItemDto>("Nom", i => i.Name),
            GridColumn.Template<DemoItemDto>("Statut", item => builder =>
            {
                var color = item.Status == "Actif" ? "#166534" : "#92400e";
                var bg = item.Status == "Actif" ? "#dcfce7" : "#fef3c7";
                builder.OpenElement(0, "span");
                builder.AddAttribute(1, "style", $"background:{bg}; color:{color}; padding:2px 8px; border-radius:12px; font-size:12px; font-weight:600;");
                builder.AddContent(2, item.Status);
                builder.CloseElement();
            }),
            GridColumn.Text<DemoItemDto>("Quantité", i => i.Count, formatter: raw => $"{raw} unités")
        ];
    }

    protected override async Task OnInitializedAsync()
    {
        Presenter.SetView(this);
        await Presenter.InitializeAsync();
    }
    protected override void OnInitialized()
    {
        InitializeContextMenu();
    }
    public void ShowError(string message) => StateHasChanged();
    public void ShowLoading(bool isLoading) => StateHasChanged();

    private void HandleUploadError(string errorMessage)
    {
        ToastService.ShowWarning(errorMessage, "Limite atteinte");
    }
    private async Task HandleFilesSelected(IReadOnlyList<IBrowserFile> files)
    {
        await Presenter.FileUploader.AddFilesAsync(files);
    }

    private void HandleRemoveFile(ClientFileInfo file)
    {
        Presenter.FileUploader.RemoveFile(file);
    }

    // 7. Libération propre
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
