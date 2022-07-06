using System;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using TelegramBot.Messages;

namespace TelegramBot.Handlers
{
    public class RegistrationHandler
    {
        [Obsolete]
        public static async void OnCallbackQweryHandlerAsync(object sender, CallbackQueryEventArgs e)
        {
            MessageCollector message = new(e.CallbackQuery.Message.Chat.Id);

            Func<Task> response = e.CallbackQuery.Data switch
            {
                _ => message.UnknownMessage()
            };

            await response();
        }

        [Obsolete]
        public static async void OnMessageHandler(object sender, MessageEventArgs e)
        {
            Console.WriteLine("Вы попали на регистрвцию :-))");
            MessageCollector message = new(e.Message.Chat.Id);

            Func<Task> response = e.Message.Text switch
            {
                _ => message.SendText("RegistrationHandler:" + e.Message.Text)
            };

            await response();
        }
    }
}
