using ImpInfCommon.ApiServices;
using ImpInfFrontCommon.Services;
using ImpInfFrontCommon.Utils;
using Microsoft.AspNetCore.Components.Authorization;

namespace ImpInfApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();
#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
#endif

            string backRoot = "http://localhost:8080/api/";

            builder.Services
                .AddTransient<CookieHandler>()
                .AddScoped(sp => sp
                    .GetRequiredService<IHttpClientFactory>()
                    .CreateClient("API"))
                .AddHttpClient("API", client => client.BaseAddress = new Uri(backRoot)).AddHttpMessageHandler<CookieHandler>();

            builder.Services.AddTransient(sp => new AuthService(backRoot, sp.GetService<HttpClient>(), "Account"));
            builder.Services.AddTransient(sp => new DaysServices(backRoot, sp.GetService<HttpClient>()));
            builder.Services.AddTransient(sp => new LessonsService(backRoot, sp.GetService<HttpClient>()));
            builder.Services.AddTransient(sp => new NewsService(backRoot, sp.GetService<HttpClient>()));
            builder.Services.AddTransient(sp => new NotesService(backRoot, sp.GetService<HttpClient>()));
            builder.Services.AddTransient(sp => new UsersService(backRoot, sp.GetService<HttpClient>()));
            builder.Services.AddTransient(sp => new UsersService(backRoot, sp.GetService<HttpClient>()));

            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore();

            builder.Services.AddScoped<AuthenticationStateProvider, TokenAuthStateProvider>();
            builder.Services.AddScoped<CookieService>();

            return builder.Build();
        }
    }
}