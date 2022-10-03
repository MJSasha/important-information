using ImpInfCommon.Data.Models;
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
        public static async Task OnCallback(object sender, CallbackQueryEventArgs queryEventArgs)
        {
            MessageCollector message = new(queryEventArgs.CallbackQuery.Message.Chat.Id, queryEventArgs.CallbackQuery.Message.MessageId);

            Task response = queryEventArgs.CallbackQuery.Data switch
            {
                "@/start" => message.EditToStartMenu(),
                "@О нас" => message.EditToAboutUsMenu(),
                "@Предметы" => message.EditToLessonsMenu(),
                "@Панель администратора" => message.EditToAdminPanel(),
                "@Создать рассылку" => Task.Run(() => DistributionService.BusyUsersIdAndService.Add(queryEventArgs.CallbackQuery.Message.Chat.Id, new MailingHandler(queryEventArgs.CallbackQuery.Message.Chat.Id))),
                "@Новости" => message.EditToWeekNews(),
                "@Календарь" => message.EditToCalendar(),
                _ => ProcessSpecialCallback(queryEventArgs.CallbackQuery.Data, message, queryEventArgs.CallbackQuery.Message.Chat.Id)
            };

            await response;
        }

        public static async Task OnMessage(object sender, MessageEventArgs eventArgs)
        {
            MessageCollector message = new(eventArgs.Message.Chat.Id, eventArgs.Message.MessageId);

            Task response = eventArgs.Message.Text switch
            {
                "/start" => message.SendStartMenu(),
                "/reg" => message.TryToStartRegistration(),
                "/passChange" => Task.Run(() => DistributionService.BusyUsersIdAndService.Add(eventArgs.Message.Chat.Id, new PasswordChangeHandler(eventArgs.Message.Chat.Id))),
                _ => ProcessSpecialMessage(eventArgs.Message.Text, message)
            };

            await response;
        }

        private static Task ProcessSpecialCallback(string callback, MessageCollector messageCollector, long chatId)
        {
            if (Regex.IsMatch(callback, @"^(@lessonId:)\d{1,}")) return messageCollector.EditToLesson(Convert.ToInt32(callback[10..]));
            else if (Regex.IsMatch(callback, @"^(@newsShift:)(-){0,1}\d{1,}")) return messageCollector.EditToWeekNews(Convert.ToInt32(callback[11..]));
            else if (Regex.IsMatch(callback, @"^(@monthShift:)(-){0,1}\d{1,}")) return messageCollector.EditToCalendar(Convert.ToInt32(callback[12..]));
            else if (Regex.IsMatch(callback, @"^(@dayDate:)\d{4}-\d{2}-\d{2}")) return messageCollector.EditToDay(DateTime.Parse(callback[9..]));
            else if (Regex.IsMatch(callback, @"^(@Редактировать название)(-){0,1}\d{1,}")) return Task.Run(() => DistributionService.BusyUsersIdAndService.Add(chatId, new RedactionHandler<Lesson>(chatId, nameof(Lesson.Name), Convert.ToInt32(callback[23..]))));
            else if (Regex.IsMatch(callback, @"^(@Редактировать преподавателя)(-){0,1}\d{1,}")) return Task.Run(() => DistributionService.BusyUsersIdAndService.Add(chatId, new RedactionHandler<Lesson>(chatId, nameof(Lesson.Teacher), Convert.ToInt32(callback[28..]))));
            else if (Regex.IsMatch(callback, @"^(@Редактировать информацию)(-){0,1}\d{1,}")) return Task.Run(() => DistributionService.BusyUsersIdAndService.Add(chatId, new RedactionHandler<Lesson>(chatId, nameof(Lesson.Information), Convert.ToInt32(callback[25..]))));
            else if (Regex.IsMatch(callback, @"^(@getNewsForLes)\d{1,}(I)\d{1,}"))
            {
                var data = callback[14..].Split('I');
                return messageCollector.SendNewsForLesson(Convert.ToInt32(data[0]), Convert.ToInt32(data[1]));
            }
            return messageCollector.UnknownMessage();
        }

        private static Task ProcessSpecialMessage(string message, MessageCollector messageCollector)
        {
            if (string.IsNullOrWhiteSpace(message)) return messageCollector.UnknownMessage();
            if (Regex.IsMatch(message, @"^(/news)\d{1,}(I)\d{1,}"))
            {
                var data = message[5..].Split('I');
                return messageCollector.SendDetailedNews(Convert.ToInt32(data[0]), Convert.ToInt32(data[1]));
            }
            return messageCollector.UnknownMessage();
        }
    }
}
