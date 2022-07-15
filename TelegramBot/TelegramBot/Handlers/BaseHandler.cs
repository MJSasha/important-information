using System;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using TelegramBot.Data;
using TelegramBot.Messages;
using TelegramBot.Services;

namespace TelegramBot.Handlers
{
    public class BaseHandler
    {
        [Obsolete]
        public static async void OnCallback(object sender, CallbackQueryEventArgs e)
        {
            MessageCollector message = new(e.CallbackQuery.Message.Chat.Id);

            Func<Task> response = e.CallbackQuery.Data switch
            {
                "@О нас" => message.EditToText(MessagesTexts.AboutUs, e.CallbackQuery.Message.MessageId),
                _ => message.UnknownMessage()
            };

            await response();
        }

        [Obsolete]
        public static async void OnMessage(object sender, MessageEventArgs e)
        {
            MessageCollector message = new(e.Message.Chat.Id);

            Func<Task> response = e.Message.Text switch
            {
                "/start" => message.StartMenu(),
                "/reg" => async () => DistributionService.BusyUsersIdAndService.Add(e.Message.Chat.Id, new RegistrationHandler(e.Message.Chat.Id)),
                _ => message.UnknownMessage()
            };

            await response();
        }
    }
}
