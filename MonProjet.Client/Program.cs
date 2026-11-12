using Blazor.WebAssembly.DynamicCulture.Loader;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MonProjet.Client.Presenters.Employee;
using MonProjet.Client.Presenters.Files;
using MonProjet.Client.Presenters.Gallery;
using MonProjet.Client.Presenters.Home;
using MonProjet.Client.Presenters.Treasury;
using MonProjet.Client.Presenters.Users;
using MonProjet.Client.Services;
using System.Globalization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped<IExceptionHandler, ExceptionHandler>();
builder.Services.AddScoped<IFocusTrapService, FocusTrapService>();
builder.Services.AddSingleton<IToastService, ToastService>();
builder.Services.AddScoped<IThemeService, ThemeService>();
builder.Services.AddScoped<ILanguageService, LanguageService>();

builder.Services.AddScoped<IHomePresenter, HomePresenter>();
builder.Services.AddScoped<IUserPresenter, UserPresenter>();
builder.Services.AddScoped<IGalleryPresenter, GalleryPresenter>();
builder.Services.AddScoped<IEmployeePresenter, EmployeePresenter>();
builder.Services.AddScoped<ITransferPresenter, TransferPresenter>();
builder.Services.AddScoped<IFileUploadPresenter, FileUploadPresenter>();
builder.Services.AddScoped<ITransactionHistoryPresenter, TransactionHistoryPresenter>();

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("fr-FR");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("fr-FR");

builder.Services.AddLocalization();

var host = builder.Build();

var languageService = host.Services.GetRequiredService<ILanguageService>();
await languageService.InitializeAsync();

var supportedCultures = new[] { new CultureInfo("fr-FR"), new CultureInfo("en-US") };
await host.LoadSatelliteCultureAssembliesCultureAsync(supportedCultures);

await host.RunAsync();