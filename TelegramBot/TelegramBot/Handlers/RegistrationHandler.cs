using System;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using TelegramBot.Messages;
using TelegramBot.Services;

namespace TelegramBot.Handlers
{
    public class RegistrationHandler
    {
        [Obsolete]
        public static async void OnCallback(object sender, CallbackQueryEventArgs e)
        {
            MessageCollector message = new(e.CallbackQuery.Message.Chat.Id);

            Func<Task> response = e.CallbackQuery.Data switch
            {
                _ => message.UnknownMessage()
            };

            await response();
        }

        [Obsolete]
        public static async void OnMessage(object sender, MessageEventArgs e)
        {
            await DistributionService.BusyUsersIdAdnService.Where(u => u.chatId == e.Message.Chat.Id).Select(u => u.registrationServices).First().ContinueRegistration(e.Message.Text);
        }
    }
}