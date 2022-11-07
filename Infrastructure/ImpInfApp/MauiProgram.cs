using ImpInfCommon.ApiServices;
using ImpInfCommon.Interfaces;
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

            string backRoot = "https://eb86-94-45-199-229.eu.ngrok.io/api/";  //https://ngrok.com/

            builder.Services
                .AddTransient<CookieHandler>()
                .AddScoped(sp => sp
                    .GetRequiredService<IHttpClientFactory>()
                    .CreateClient("API"))
                .AddHttpClient("API", client => client.BaseAddress = new Uri(backRoot)).AddHttpMessageHandler<CookieHandler>();

            builder.Services.AddTransient(sp => new AuthService(backRoot, sp.GetService<HttpClient>(), "Account"))
                .AddTransient<IDays, DaysServices>(sp => new DaysServices(backRoot, sp.GetService<HttpClient>()))
                .AddTransient<ILesson, LessonsService>(sp => new LessonsService(backRoot, sp.GetService<HttpClient>()))
                .AddTransient<INews, NewsService>(sp => new NewsService(backRoot, sp.GetService<HttpClient>()))
                .AddTransient<INotes, NotesService>(sp => new NotesService(backRoot, sp.GetService<HttpClient>()))
                .AddTransient<IUser, UsersService>(sp => new UsersService(backRoot, sp.GetService<HttpClient>()));

            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore();

            builder.Services.AddScoped<AuthenticationStateProvider, TokenAuthStateProvider>()
                            .AddScoped<CookieService>();

            builder.Services.AddSingleton<ErrorsHandler>();

            return builder.Build();
        }
    }
}