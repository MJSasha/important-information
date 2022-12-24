using ImpInfFrontCommon;

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
            ServiceBuilder.RegistrateCommonServices(builder.Services, backRoot);
            return builder.Build();
        }
    }
}