using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using TelegramBot.Messages;
using TelegramBot.Services;

namespace TelegramBot.Handlers
{
    public class BaseHandler
    {
        [Obsolete]
        public static async Task OnCallback(object sender, CallbackQueryEventArgs e)
        {
            MessageCollector message = new(e.CallbackQuery.Message.Chat.Id, e.CallbackQuery.Message.MessageId);

            Task response = e.CallbackQuery.Data switch
            {
                "@/start" => message.EditToStartMenu(),
                "@О нас" => message.EditToAboutUsMenu(),
                "@Предметы" => message.EditToLessonsMenu(),
                "@Отправить всем" => Task.Run(() => DistributionService.BusyUsersIdAndService.Add(e.CallbackQuery.Message.Chat.Id, new MailingHandler(e.CallbackQuery.Message.Chat.Id))),
                _ => message.UnknownMessage()
                _ => ProcessSpecialCallback(e.CallbackQuery.Data, message)
            };

            await response;
        }

        [Obsolete]
        public static async Task OnMessage(object sender, MessageEventArgs e)
        {
            MessageCollector message = new(e.Message.Chat.Id, e.Message.MessageId);

            Task response = e.Message.Text switch
            {
                "/start" => message.SendStartMenu(),
                "/reg" => Task.Run(() => DistributionService.BusyUsersIdAndService.Add(e.Message.Chat.Id, new RegistrationHandler(e.Message.Chat.Id))),
                "/passChange" => Task.Run(() => DistributionService.BusyUsersIdAndService.Add(e.Message.Chat.Id, new PasswordChangeHandler(e.Message.Chat.Id))),
                _ => message.UnknownMessage()
            };

            await response;
        }

        private static Task ProcessSpecialCallback(string callback, MessageCollector message)
        {
            if (Regex.IsMatch(callback, @"^(@lessonId:)[0-9]{1,}")) return message.EditToLesson(Convert.ToInt32(callback[10..]));
            return message.UnknownMessage();
        }
    }
}
