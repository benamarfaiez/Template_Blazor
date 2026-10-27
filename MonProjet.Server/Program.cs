using MonProjet.Client.Presenters.Gallery;
using MonProjet.Client.Presenters.Home;
using MonProjet.Client.Presenters.Users;
using MonProjet.Client.Services;
using MonProjet.Server.Components;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddScoped<IHomePresenter, HomePresenter>();
builder.Services.AddScoped<IUserPresenter, UserPresenter>();
builder.Services.AddScoped<IGalleryPresenter, GalleryPresenter>();
builder.Services.AddScoped<IExceptionHandler, ExceptionHandler>();
builder.Services.AddScoped<IFocusTrapService, FocusTrapService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(MonProjet.Client._Imports).Assembly);

app.Run();