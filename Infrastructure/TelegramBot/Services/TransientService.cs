using ImpInfCommon.ApiServices;
using System;
using System.Net;
using System.Net.Http;

namespace TelegramBot.Services
{
    public static class TransientService
    {
        public static AuthService GetAuthService() => new AuthService(AppSettings.BackRoot, GetClient(), "Account");
        public static DaysServices GetDaysServices() => new DaysServices(AppSettings.BackRoot, GetClient());
        public static LessonsService GetLessonsService() => new LessonsService(AppSettings.BackRoot, GetClient());
        public static NewsService GetNewsService() => new NewsService(AppSettings.BackRoot, GetClient());
        public static NotesService GetNotesService() => new NotesService(AppSettings.BackRoot, GetClient());
        public static UsersService GetUsersService() => new UsersService(AppSettings.BackRoot, GetClient());

        public static HttpClient GetClient()
        {
            if (!string.IsNullOrWhiteSpace(AppSettings.ApiToken))
            {
                HttpClientHandler handler = new()
                {
                    CookieContainer = new CookieContainer()
                };
                handler.CookieContainer.Add(new Uri(AppSettings.BackRoot), new Cookie("token", AppSettings.ApiToken));
                return new HttpClient(handler);
            }
            return new HttpClient();
        }
    }
}
