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
            ButtonsGenerator buttonsGenerator = new();
            buttonsGenerator.SetInlineButtons(new List<List<string>>()
            {
                new List<string>{ "Новости" },
                new List<string>{ "О нас" },
            });

            buttonsGenerator.SetInlineUrlButtons(new List<List<(string, string)>>()
            {
                new List<(string, string)>{("Сайт", AppSettings.FrontRoot) }
            });

            await bot.SendMessage("Доброе пожаловать в чат Важной информации.\nЧто бы вы хотели узнать?", buttonsGenerator.GetButtons());
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