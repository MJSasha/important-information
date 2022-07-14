using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TelegramBot.Interfaces;
using TelegramBot.Services;

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
                new List<string>{ "Новости" },
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
        public Func<Task> SubjectMenu(int messageid)
        {
            List<List<string>> markup = new()
            {
                new List<string> { "Предмет1", "Предмет2", "Предмет3" },
                new List<string> { "Предмет4", "Предмет5" } ,
                new List<string> { "Назад в стартовое меню"}
            };

            return () => bot.EditMessage("Выберите предмет", ButtonsGenerater.GetInlineButtons(markup), messageid);
        }
            public Func<Task> SubjectInfo(int messageid)
        {
            List<List<string>> markup = new()
            {
                new List<string> { "Назад" },
                new List<string> { "Назад в стартовое меню" }
            };

            return () => bot.EditMessage("Название:\nПреподаватель:\nИнформация:", ButtonsGenerater.GetInlineButtons(markup), messageid);
        }
        public Func<Task> SendText(string text)
        {
            return () => bot.SendMessage(text);
        }
        public Func<Task> UnknownMessage()
        {
            return () => bot.SendMessage("Пока я не понимаю данное сообщение, но скоро научусь");
        }
    }
}