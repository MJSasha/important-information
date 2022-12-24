using ImpInfCommon.ApiServices;
using System;
using System.Net;
using System.Net.Http;

namespace TelegramBot.Services
{
    public static class SingletontService
    {
        public static ErrorsHandler ErrorsHandler { get; } = new();

        private static AuthService authService;
        private static DaysServices daysServices;
        private static LessonsService lessonsService;
        private static NewsService newsService;
        private static NotesService notesService;
        private static UsersService usersService;

        public static AuthService GetAuthService() => authService ??= new AuthService(CreateClient(), ErrorsHandler);
        public static DaysServices GetDaysServices() => daysServices ??= new DaysServices(CreateClient(), ErrorsHandler);
        public static LessonsService GetLessonsService() => lessonsService ??= new LessonsService(CreateClient(), ErrorsHandler);
        public static NewsService GetNewsService() => newsService ??= new NewsService(CreateClient(), ErrorsHandler);
        public static NotesService GetNotesService() => notesService ??= new NotesService(CreateClient(), ErrorsHandler);
        public static UsersService GetUsersService() => usersService ??= new UsersService(CreateClient(), ErrorsHandler);

        public static HttpClient CreateClient()
        {
            if (!string.IsNullOrWhiteSpace(AppSettings.ApiToken))
            {
                HttpClientHandler handler = new()
                {
                    CookieContainer = new CookieContainer()
                };
                handler.CookieContainer.Add(new Uri(AppSettings.BackRoot), new Cookie("token", AppSettings.ApiToken));
                return new HttpClient(handler)
                {
                    BaseAddress= new Uri(AppSettings.BackRoot)
                };
            }
            return new HttpClient()
            {
                BaseAddress= new Uri(AppSettings.BackRoot)
            };
        }
    }
}
