using Microsoft.AspNetCore.Localization;
using MonProjet.Client.Presenters.Employee;
using MonProjet.Client.Presenters.Files;
using MonProjet.Client.Presenters.Gallery;
using MonProjet.Client.Presenters.Home;
using MonProjet.Client.Presenters.Treasury;
using MonProjet.Client.Presenters.Users;
using MonProjet.Client.Services;
using MonProjet.Server.Components;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddScoped<IExceptionHandler, ExceptionHandler>();
builder.Services.AddScoped<IFocusTrapService, FocusTrapService>();
builder.Services.AddSingleton<IToastService, ToastService>();
builder.Services.AddScoped<IThemeService, ThemeService>();

builder.Services.AddScoped<IHomePresenter, HomePresenter>();
builder.Services.AddScoped<IUserPresenter, UserPresenter>();
builder.Services.AddScoped<IGalleryPresenter, GalleryPresenter>();
builder.Services.AddScoped<IEmployeePresenter, EmployeePresenter>();
builder.Services.AddScoped<ITransferPresenter, TransferPresenter>();
builder.Services.AddScoped<IFileUploadPresenter, FileUploadPresenter>();
builder.Services.AddScoped<ITransactionHistoryPresenter, TransactionHistoryPresenter>();

builder.Services.AddLocalization();
builder.Services.AddScoped<ILanguageService, LanguageService>();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

var supportedCultures = new[] { "en-US", "fr-FR" };
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("fr-FR");
    options.SupportedCultures = [.. supportedCultures.Select(c => new CultureInfo(c))];
    options.SupportedUICultures = options.SupportedCultures;
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseRequestLocalization();
app.UseHttpsRedirection();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(MonProjet.Client._Imports).Assembly);

app.Run();