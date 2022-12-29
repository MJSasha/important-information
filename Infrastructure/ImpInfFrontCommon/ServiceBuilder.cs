using ImpInfCommon.ApiServices;
using ImpInfCommon.Interfaces;
using ImpInfFrontCommon.Services;
using ImpInfFrontCommon.Utils;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace ImpInfFrontCommon
{
    public static class ServiceBuilder
    {
        public static async void RegistrateCommonServices(IServiceCollection services, string backRoot)
        {
            services.AddTransient<CookieHandler>()
                .AddTransient(sp => sp
                    .GetRequiredService<IHttpClientFactory>()
                    .CreateClient("API"))
                .AddHttpClient("API", client => client.BaseAddress = new Uri(backRoot)).AddHttpMessageHandler<CookieHandler>();

            services.AddTransient<IAuthService, AuthService>()
                            .AddTransient<IDaysService, DaysServices>()
                            .AddTransient<ILessonService, LessonsService>()
                            .AddTransient<INewsService, NewsService>()
                            .AddTransient<INotesService, NotesService>()
                            .AddTransient<IUserService, UsersService>();

            services.AddOptions();
            services.AddAuthorizationCore();

            services.AddScoped<AuthenticationStateProvider, TokenAuthStateProvider>()
                            .AddScoped<CookieService>();

            services.AddSingleton<IErrorsHandler, ErrorsHandler>();
            services.AddSingleton<DialogService>();

            await NotificationsService.Init($"{backRoot}/hubs/NotificationsService");
        }
    }
}
