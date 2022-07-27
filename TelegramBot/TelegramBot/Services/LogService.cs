using System;
using Telegram.Bot.Args;

namespace TelegramBot.Services
{
    public static class LogService
    {
        public static void LogStart()
        {
            Console.ResetColor();
            Console.WriteLine($"{DateTime.Now} BOT START" +
                $"\n-----------------------------------------------------" +
                $"\nToken: {AppSettings.Token}" +
                $"\nBackEnd Root: {AppSettings.BackRoot}" +
                $"\n-----------------------------------------------------\n");
        }

        public static void LogInfo(string info)
        {
            BaseLog(LogType.INFO, info);
        }
        public static void LogError(string error)
        {
            BaseLog(LogType.ERROR, error);
        }


        public static void LogServerNotFound(string actionName)
        {
            LogError($"Server not found (404). {actionName} - Not completed");
        }

        [Obsolete]
        public static void LogMessages(object sender, MessageEventArgs e)
        {
            LogInfo($"ChatId: { e.Message.Chat.Id} | Message: { e.Message.Text}");
        }

        [Obsolete]
        public static void LogCallbacks(object sender, CallbackQueryEventArgs e)
        {
            LogInfo($"ChatId: {e.CallbackQuery.Message.Chat.Id} | Callback: {e.CallbackQuery.Data}");
        }

        private static void BaseLog(LogType logType, string message)
        {
            Console.Write(DateTime.Now);
            Console.ForegroundColor = logType.GetCollor();
            Console.Write($" {logType}");
            Console.ResetColor();
            Console.WriteLine($" --- {message}");
        }

        private enum LogType
        {
            INFO,
            ERROR
        }

        private static ConsoleColor GetCollor(this LogType logType)
        {
            return logType switch
            {
                LogType.INFO => ConsoleColor.Green,
                LogType.ERROR => ConsoleColor.Red,
                _ => ConsoleColor.White
            };
        }
    }
}
