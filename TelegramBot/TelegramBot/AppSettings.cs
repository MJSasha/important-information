using System;

namespace TelegramBot
{
    public static class AppSettings
    {
        public static string BotToken { get; } = Environment.GetEnvironmentVariable("API_TOKEN") ?? "2065215367:AAHxs51AowRJAqefe3tvV7d5jn5nsC_-xDc";
        public static string ApiToken { get; } = Environment.GetEnvironmentVariable("API_TOKEN") ?? "Fp9u5dsvcdM3XIm";
        public static string BackRoot { get; } = Environment.GetEnvironmentVariable("BACK_ROOT") ?? "http://localhost:8080/api/";
        public static string FrontRoot { get; } = Environment.GetEnvironmentVariable("FRONT_ROOT") ?? "google.com";

        public static string UsersRoot { get; } = "users";
        public static string DaysRoot { get; } = "days";
        public static string NewsRoot { get; } = "news";
        public static string NotesRoot { get; } = "notes";
        public static string AuthRoot { get; } = "auth";
        public static string LessonsRoot { get; } = "lessons";
    }
}
