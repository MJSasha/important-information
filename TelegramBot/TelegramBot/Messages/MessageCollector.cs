using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TelegramBot.Interfaces;
using TelegramBot.Services;
using TelegramBot.Services.ApiServices;
using TelegramBot.Data.Models;



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
                new List<string>{ "Hello", "L1B2", "L1B3","extra","Данек вперед" },
                new List<string>{ "L2B1", "L2B2" },
                new List<string>{ "Отправить всем" }
            };

            return () => bot.SendMessage("Стартовое меню", ButtonsGenerater.GetInlineButtons(markup));
        }
        public Func<Task> SendText(string text)
        {
            return () => bot.SendMessage(text);
        }
        public Func<Task> UnknownMessage()
        {
            return () => bot.SendMessage("Пока я не понимаю данное сообщение, но скоро научусь");
        }

        [Obsolete]
        public Func<Task> SendAll(string text)
        {
            News news = new News();
            news.NeedToSend = true;
            news.Message = text;
            

            NewsService newsService = new NewsService();    //Отправляем запросс на добавление параметров news
            newsService.Add(news);
            return () => bot.SendMessage("Сообщение отправленно");


        }
    }
}
