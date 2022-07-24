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

        public async Task StartMenu()
        {
            List<List<string>> markup = new()
            {
                new List<string>{ "О нас" },
                new List<string>{ "Сменить пароль" },
            };

            await bot.SendMessage("Доброе пожаловать в чат Важной информации.\nЧто бы вы хотели узнать?", ButtonsGenerater.GetInlineButtons(markup));
        }

        public async Task SendText(string text)
        {
            await bot.SendMessage(text);
        }

        public async Task EditToText(string text, int messageId)
        {
            await bot.EditMessage(text, messageId);
        }

        public async Task UnknownMessage()
        {
            await bot.SendMessage("Пока я не понимаю данное сообщение, но скоро научусь");
        }
    }
}