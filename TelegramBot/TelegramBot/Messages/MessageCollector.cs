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
            };

            List<List<string>> urlmarkup = new()
            {
                new List<string>{"Go URL", "https://www.google.com"}
            };
            //InlineKeyboardButton urlButton = new InlineKeyboardButton();
            //urlButton.Text = "Go URL";
            //urlButton.Url = "https://www.google.com";

            return () => bot.SendMessage("Доброе пожаловать в чат Важной информации.\nЧто бы вы хотели узнать?", ButtonsGenerater.GetInlineButtons(markup), ButtonsGenerater.GetInlineUrlButtons(urlmarkup));
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