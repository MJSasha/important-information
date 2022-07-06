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
        public List<long> busyUsers;

        [Obsolete]
        public MessageCollector(long chatId)
        {
            bot = new BotService(chatId);
            busyUsers = new List<long>();
        }

        public Func<Task> StartMenu()
        {
            List<List<string>> markup = new()
            {
                new List<string>{ "L1B1", "L1B2", "L1B3" },
                new List<string>{ "L2B1", "L2B2" },
                new List<string>{ "L3B1" }
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
        public Func<Task> RegistrationUsers(long chatId) //AddToRegistration
        {
            if (!busyUsers.Contains(chatId)) busyUsers.Add(chatId);
            return () => bot.SendMessage("Пользователь " + chatId.ToString() + "улетел на регистрацию");
        }
        public async Task RegistrationUsers(long chatId, string text) //получение содержимого сообщений из основного ообработчика
        {
            //
            await bot.SendMessage("Пользователь: " + chatId.ToString() + "text: " + text);
        }
    }
}




