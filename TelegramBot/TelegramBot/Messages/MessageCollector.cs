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
            ButtonsGenerater buttonsGenerater = new();
            buttonsGenerater.SetInlineButtons(new List<List<string>>()
            {
                new List<string>{ "Новости" },
                new List<string>{ "О нас" },
            });
            buttonsGenerater.SetInlineUrlButtons(new List<List<(string, string)>>()
            {
                new List<(string, string)>{("Go URL", "https://www.google.com")}
            });

            return () => bot.SendMessage("Доброе пожаловать в чат Важной информации.\nЧто бы вы хотели узнать?", buttonsGenerater.GetButtons());
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