using ImpInfCommon.ApiServices;

namespace TelegramBot.Services
{
    public static class TransientService
    {
        public static AuthService GetAuthService() => new AuthService(AppSettings.BackRoot, AppSettings.AuthRoot, AppSettings.ApiToken);
        public static DaysServices GetDaysServices() => new DaysServices(AppSettings.BackRoot, AppSettings.ApiToken);
        public static LessonsService GetLessonsService() => new LessonsService(AppSettings.BackRoot, AppSettings.ApiToken);
        public static NewsService GetNewsService() => new NewsService(AppSettings.BackRoot, AppSettings.ApiToken);
        public static NotesService GetNotesService() => new NotesService(AppSettings.BackRoot, AppSettings.ApiToken);
        public static UsersService GetUsersService() => new UsersService(AppSettings.BackRoot, AppSettings.ApiToken);
    }
}
