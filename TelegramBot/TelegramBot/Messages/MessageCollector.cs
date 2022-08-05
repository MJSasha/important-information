using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TelegramBot.Interfaces;
using TelegramBot.Services;
using TelegramBot.Services.ApiServices;
using TelegramBot.Data.Models;
using System.Linq;
using TelegramBot.Handlers;

namespace TelegramBot.Messages
{
    public class MessageCollector
    {
        private readonly IBotService bot;
        private readonly long chatId;

        [Obsolete]
        public MessageCollector(long chatId)
        {
            bot = new BotService(chatId);
            this.chatId = chatId;
        }

        public async Task StartMenu()
        {
            var usersService = new UsersService();
            var currentUser = (await usersService.Get()).FirstOrDefault(u => u.ChatId == chatId);
            if (currentUser.Role == Role.ADMIN)
            {
                List<List<string>> markup = new()
            {
                new List<string>{ "О нас" },
                new List<string>{ "Отправить всем" },
            };
                await bot.SendMessage("Доброе пожаловать в чат Важной информации.\nЧто бы вы хотели узнать?", ButtonsGenerater.GetInlineButtons(markup));
            }
            else
            {
                List<List<string>> markup = new()
            {
                new List<string>{ "О нас" },
            };
                await bot.SendMessage("Доброе пожаловать в чат Важной информации.\nЧто бы вы хотели узнать?", ButtonsGenerater.GetInlineButtons(markup));
            }
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