using System;

namespace TelegramBot
{
    public static class AppSettings
    {
        public static string BotToken { get; } = Environment.GetEnvironmentVariable("BOT_TOKEN") ?? "2065215367:AAHxs51AowRJAqefe3tvV7d5jn5nsC_-xDc";
        public static string ApiToken { get; } = Environment.GetEnvironmentVariable("API_TOKEN") ?? "12345";
        public static string BackRoot { get; } = Environment.GetEnvironmentVariable("BACK_ROOT") ?? "http://localhost:8080/api/";
        public static string FrontRoot { get; } = Environment.GetEnvironmentVariable("FRONT_ROOT") ?? "google.com";

        public static string AuthRoot { get; } = "Account";
    }
}
