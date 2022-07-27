using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TelegramBot.Data.ViewModels;
using TelegramBot.Interfaces;
using TelegramBot.Services;
using TelegramBot.Services.ApiServices;

namespace TelegramBot.Messages
{
    public class MessageCollector
    {
        private readonly IBotService bot;

        [Obsolete]
        public MessageCollector(long chatId)
        {
            bot = new BotService(chatId);
        }

        public Func<Task> StartMenu()
        {
            List<List<string>> markup = new()
            {
                new List<string>{ "О нас" },
                new List<string> { "Предметы" },
            };

            return () => bot.SendMessage("Доброе пожаловать в чат Важной информации.\nЧто бы вы хотели узнать?", ButtonsGenerater.GetInlineButtons(markup));
        }
        public Func<Task> ReturnStartMenu(int messageid)
        {
            List<List<string>> markup = new()
            {
                new List<string> { "Новости" },
                new List<string> { "О нас" },
                new List<string> { "Предметы" },
            };

            return () => bot.EditMessage("Доброе пожаловать в чат Важной информации.\nЧто бы вы хотели узнать?", ButtonsGenerater.GetInlineButtons(markup), messageid);
        }
       
        public static async Task<List<string>> GetLessonsName()
        {
            var lessonsService = new LessonsService();
            var lessons = await lessonsService.Get();
            return lessons.Select(x => x.Name).ToList();
        }

        public async Task<Func<Task>> LessonsMenu(int messageid)
        {
            List<string> lessonNames = await GetLessonsName();
            List<List<(string, string)>> markup = new();
            
                for (int i = 0; i < lessonNames.Count; i += 3)
                {
                    markup.Add(new List<(string, string)> { (lessonNames[i], lessonNames[i]), (lessonNames[i + 1], lessonNames[i]), (lessonNames[i + 2], lessonNames[i]) });

                }
                markup.Add(new List<(string, string)> { ("Назад", "основное меню") });

            return () => bot.EditMessage("Выберите предмет", ButtonsGenerater.GetInlineButtons(markup), messageid);
            
        }
        public Func<Task> LessonInfo(int messageid)
        {

            List<List<(string, string)>> markup = new()
            {
                new List<(string, string)> { ("Назад", "меню предметов") }
            };

                return () => bot.EditMessage($"Название:\nПреподаватель:\nИнформация:", ButtonsGenerater.GetInlineButtons(markup), messageid);
        }
        public Func<Task> SendText(string text)
        {
            return () => bot.SendMessage(text) ;
        }

        public Func<Task> EditToText(string text, int messageId)
        {
            return () => bot.EditMessage(text, messageId);
        }

        public Func<Task> UnknownMessage()
        {
            return () => bot.SendMessage("Пока я не понимаю данное сообщение, но скоро научусь");
        }
    }
}