using System;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using TelegramBot.Data.ViewModels;
using TelegramBot.Messages;
using TelegramBot.Services;

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
            Console.WriteLine(e.Message.Chat.FirstName + " - попал на регистрацию -");
            var regService = new RegistrationServices();

            await regService.StartRegistration(e.Message.Chat.Id, e.Message.Text);
        }
    }
}