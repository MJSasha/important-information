namespace TelegramBot
{
    public static class AppSettings
    {
#if DEBUG
        public static string Token { get; } = "5559902107:AAGDtKm7uQ9RVHKrgi5-4n5tK_X8jjBatcc";
        public static string ApiToken { get; } = "Fp9u5dsvcdM3XIm";
        public static string BaseRoot { get; } = "http://localhost:8080/api/";
#else
        public static string Token { get; } = "";
        public static string ApiToken { get; } = "";
        public static string BaseRoot { get; } = "";
#endif

        public static string UsersRoot { get; } = "users";
        public static string DaysRoot { get; } = "days";
        public static string NewsRoot { get; } = "news";
        public static string NotesRoot { get; } = "notes";
        public static string AuthRoot { get; } = "auth";
    }
}
