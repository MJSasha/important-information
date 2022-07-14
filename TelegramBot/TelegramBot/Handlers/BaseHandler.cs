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
        public static async void OnCallbackQweryHandlerAsync(object sender, CallbackQueryEventArgs e)
        {
            MessageCollector message = new(e.CallbackQuery.Message.Chat.Id);

            Func<Task> response = e.CallbackQuery.Data switch
            {
                "@О нас" => message.SendText(MessagesTexts.AboutUs),
                "@Предметы" => message.SubjectMenu(e.CallbackQuery.Message.MessageId),
                "@Предмет1" => message.SubjectInfo(e.CallbackQuery.Message.MessageId),
                "@Предмет2" => message.SubjectInfo(e.CallbackQuery.Message.MessageId),
                "@Предмет3" => message.SubjectInfo(e.CallbackQuery.Message.MessageId),
                "@Предмет4" => message.SubjectInfo(e.CallbackQuery.Message.MessageId),
                "@Предмет5" => message.SubjectInfo(e.CallbackQuery.Message.MessageId),
                "@Назад в стартовое меню" => message.ReturnStartMenu(e.CallbackQuery.Message.MessageId),
                "@Назад" => message.SubjectMenu(e.CallbackQuery.Message.MessageId),
                _ => message.UnknownMessage()
            };

            await response();
        }

        [Obsolete]
        public static async void OnMessageHandler(object sender, MessageEventArgs e)
        {
            MessageCollector message = new(e.Message.Chat.Id);

            Func<Task> response = e.Message.Text switch
            {
                "/start" => message.StartMenu(),
                "/reg" => async () => DistributionService.BusyUsersIdAdnService.Add((e.Message.Chat.Id, new RegistrationServices(e.Message.Chat.Id))),
                "Привет" => message.SendText("Привет"),
                _ => message.UnknownMessage()
            };

            await response();
        }
    }
}
