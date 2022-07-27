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
            ButtonsGenerater buttonsGenerater = new();
            buttonsGenerater.SetInlineButtons(new List<List<string>>()
            {
                new List<string>{ "Новости" },
                new List<string>{ "О нас" },
            });

            buttonsGenerater.SetInlineUrlButtons(new List<List<(string, string)>>()
            {
            new List<(string, string)> { ("Сайт", AppSettings.FrontRoot) }
            });

            await bot.SendMessage("Доброе пожаловать в чат Важной информации.\nЧто бы вы хотели узнать?", buttonsGenerater.GetButtons());
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