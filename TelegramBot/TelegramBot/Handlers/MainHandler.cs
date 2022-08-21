using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using TelegramBot.Messages;
using TelegramBot.Services;

namespace TelegramBot.Handlers
{
    public class MainHandler
    {
        [Obsolete]
        public static async Task OnCallback(object sender, CallbackQueryEventArgs queryEventArgs)
        {
            MessageCollector message = new(queryEventArgs.CallbackQuery.Message.Chat.Id, queryEventArgs.CallbackQuery.Message.MessageId);

            Task response = queryEventArgs.CallbackQuery.Data switch
            {
                "@/start" => message.EditToStartMenu(),
                "@О нас" => message.EditToAboutUsMenu(),
                "@Предметы" => message.EditToLessonsMenu(),
                "@Отправить всем" => Task.Run(() => DistributionService.BusyUsersIdAndService.Add(queryEventArgs.CallbackQuery.Message.Chat.Id, new MailingHandler(queryEventArgs.CallbackQuery.Message.Chat.Id))),
                "@Новости" => message.SendWeekNews(),
                _ => ProcessSpecialCallback(queryEventArgs.CallbackQuery.Data, message)
            };

            await response;
        }

        [Obsolete]
        public static async Task OnMessage(object sender, MessageEventArgs eventArgs)
        {
            MessageCollector message = new(eventArgs.Message.Chat.Id, eventArgs.Message.MessageId);

            Task response = eventArgs.Message.Text switch
            {
                "/start" => message.SendStartMenu(),
                "/reg" => Task.Run(() => DistributionService.BusyUsersIdAndService.Add(eventArgs.Message.Chat.Id, new RegistrationHandler(eventArgs.Message.Chat.Id))),
                "/passChange" => Task.Run(() => DistributionService.BusyUsersIdAndService.Add(eventArgs.Message.Chat.Id, new PasswordChangeHandler(eventArgs.Message.Chat.Id))),
                _ => ProcessSpecialMessage(eventArgs.Message.Text, message)
            };

            await response;
        }

        private static Task ProcessSpecialCallback(string callback, MessageCollector message)
        {
            if (Regex.IsMatch(callback, @"^(@lessonId:)[0-9]{1,}")) return message.EditToLesson(Convert.ToInt32(callback[10..]));
            else if (Regex.IsMatch(callback, @"^(@newsShift:)(-){0,1}[0-9]{1,}")) return message.SendWeekNews(Convert.ToInt32(callback[11..]));
            else if (Regex.IsMatch(callback, @"^(@getNewsForLes:)[0-9]{1,}")) return message.SendWeekNews(Convert.ToInt32(callback[15..]));
            return message.UnknownMessage();
        }
    }
}
