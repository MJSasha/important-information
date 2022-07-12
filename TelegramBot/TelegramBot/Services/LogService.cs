using System;
using Telegram.Bot.Args;

namespace TelegramBot.Services
{
    public static class LogService
    {
        public static void LogStart()
        {
            Console.WriteLine($"{DateTime.Now} BOT START" +
                $"\n-----------------------------------------------------" +
                $"\nToken: {AppSettings.Token}" +
                $"\nBackEnd Root: {AppSettings.BaseRoot}" +
                $"\n-----------------------------------------------------");
        }

        public static void LogInfo(string info)
        {
            Console.WriteLine($"{DateTime.Now} INFO --- {info}");
        }

        public static void LogServerNotFound(string actionName = null)
        {
            Console.WriteLine($"{DateTime.Now} ERROR --- Server not found (404). {actionName} - Not completed");
        }

        [Obsolete]
        public static void LogMessages(object sender, MessageEventArgs e)
        {
            Console.WriteLine($"{DateTime.Now} INFO --- ChatId: {e.Message.Chat.Id} | Message: {e.Message.Text}");
        }

        [Obsolete]
        public static void LogCallbacks(object sender, CallbackQueryEventArgs e)
        {
            Console.WriteLine($"{DateTime.Now} INFO --- ChatId: {e.CallbackQuery.Message.Chat.Id} | Callback: {e.CallbackQuery.Data}");
        }
    }
}
