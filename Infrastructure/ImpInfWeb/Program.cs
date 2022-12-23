using ImpInfCommon.ApiServices;
using ImpInfCommon.Interfaces;
using ImpInfFrontCommon;
using ImpInfFrontCommon.Services;
using ImpInfFrontCommon.Utils;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

string backRoot = "http://localhost:8080/api";

builder.Services
    .AddTransient<CookieHandler>()
    .AddScoped(sp => sp
        .GetRequiredService<IHttpClientFactory>()
        .CreateClient("API"))
    .AddHttpClient("API", client => client.BaseAddress = new Uri(backRoot)).AddHttpMessageHandler<CookieHandler>();

builder.Services.AddTransient<IAuthService, AuthService>()
                .AddTransient<IDaysService, DaysServices>()
                .AddTransient<ILessonService, LessonsService>()
                .AddTransient<INewsService, NewsService>()
                .AddTransient<INotesService, NotesService>()
                .AddTransient<IUserService, UsersService>();

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<AuthenticationStateProvider, TokenAuthStateProvider>()
                .AddScoped<CookieService>();

builder.Services.AddSingleton<IErrorsHandler, ErrorsHandler>();
builder.Services.AddSingleton<DialogService>();


var app = builder.Build().RunAsync();