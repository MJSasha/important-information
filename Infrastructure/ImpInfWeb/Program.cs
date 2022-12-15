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

string backRoot = "http://localhost:8080/api/";

builder.Services
    .AddTransient<CookieHandler>()
    .AddScoped(sp => sp
        .GetRequiredService<IHttpClientFactory>()
        .CreateClient("API"))
    .AddHttpClient("API", client => client.BaseAddress = new Uri(backRoot)).AddHttpMessageHandler<CookieHandler>();

builder.Services.AddTransient<IAuthService, AuthService>(sp => new AuthService(backRoot, sp.GetService<HttpClient>(), "Account"))
                .AddTransient<IDaysService, DaysServices>(sp => new DaysServices(backRoot, sp.GetService<HttpClient>()))
                .AddTransient<ILessonService, LessonsService>(sp => new LessonsService(backRoot, sp.GetService<HttpClient>()))
                .AddTransient<INewsService, NewsService>(sp => new NewsService(backRoot, sp.GetService<HttpClient>()))
                .AddTransient<INotesService, NotesService>(sp => new NotesService(backRoot, sp.GetService<HttpClient>()))
                .AddTransient<IUserService, UsersService>(sp => new UsersService(backRoot, sp.GetService<HttpClient>()));

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<AuthenticationStateProvider, TokenAuthStateProvider>()
                .AddScoped<CookieService>();

builder.Services.AddSingleton<ErrorsHandler>();
builder.Services.AddSingleton<DialogService>();


var app = builder.Build().RunAsync();