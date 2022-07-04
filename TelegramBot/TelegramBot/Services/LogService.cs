using System;
using Telegram.Bot.Args;

namespace TelegramBot.Services
{
    public static class LogService
    {
        [Obsolete]
        public static void MessageLogging(object sender, MessageEventArgs e)
        {
            Console.WriteLine($"{DateTime.Now} INFO --- ChatId: { e.Message.Chat.Id}; Message: { e.Message.Text}");
        }

        [Obsolete]
        public static void CallbackLogging(object sender, CallbackQueryEventArgs e)
        {
            Console.WriteLine($"{DateTime.Now} INFO --- ChatId: {e.CallbackQuery.Message.Chat.Id}; Callback: {e.CallbackQuery.Data}");
        }
    }
}
