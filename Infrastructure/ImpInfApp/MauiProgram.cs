using ImpInfCommon.ApiServices;

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

            builder.Services.AddTransient(sp => new AuthService(backRoot, "Account"));
            builder.Services.AddTransient(sp => new DaysServices(backRoot));
            builder.Services.AddTransient(sp => new LessonsService(backRoot));
            builder.Services.AddTransient(sp => new NewsService(backRoot));
            builder.Services.AddTransient(sp => new NotesService(backRoot));
            builder.Services.AddTransient(sp => new UsersService(backRoot));

            return builder.Build();
        }
    }
}