using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MonProjet.Client.Presenters.Gallery;
using MonProjet.Client.Presenters.Home;
using MonProjet.Client.Presenters.Users;
using MonProjet.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped<IExceptionHandler, ExceptionHandler>();
builder.Services.AddScoped<IHomePresenter, HomePresenter>();
builder.Services.AddScoped<IUserPresenter, UserPresenter>();
builder.Services.AddScoped<IGalleryPresenter, GalleryPresenter>();

await builder.Build().RunAsync();