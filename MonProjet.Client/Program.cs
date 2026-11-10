using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MonProjet.Client.Presenters.Employee;
using MonProjet.Client.Presenters.Files;
using MonProjet.Client.Presenters.Gallery;
using MonProjet.Client.Presenters.Home;
using MonProjet.Client.Presenters.Treasury;
using MonProjet.Client.Presenters.Users;
using MonProjet.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

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

await builder.Build().RunAsync();