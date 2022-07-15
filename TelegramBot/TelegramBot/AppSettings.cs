namespace TelegramBot
{
    public static class AppSettings
    {
#if DEBUG
        public static string Token { get; } = "2065215367:AAHxs51AowRJAqefe3tvV7d5jn5nsC_-xDc";
        public static string ApiToken { get; } = "Fp9u5dsvcdM3XIm";
        public static string BaseRoot { get; } = "http://localhost:8080/api/";
#else
        public static string Token { get; } = "5585844903:AAE9kG6Lsar77FVDuObq9TUDr99FrB-0FSI";
        public static string ApiToken { get; } = "Fp9u5dsvcdM3XIm";
        public static string BaseRoot { get; } = "https://a8815-e735.s.d-f.pw/api/";
#endif

        public static string UsersRoot { get; } = "users";
        public static string DaysRoot { get; } = "days";
        public static string NewsRoot { get; } = "news";
        public static string NotesRoot { get; } = "notes";
        public static string AuthRoot { get; } = "auth";
    }
}
