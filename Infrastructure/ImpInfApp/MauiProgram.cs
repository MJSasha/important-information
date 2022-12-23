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

            string backRoot = "https://eb86-94-45-199-229.eu.ngrok.io/api";  //https://ngrok.com/

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

            return builder.Build();
        }
    }
}