using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TelegramBot.Data;
using TelegramBot.Interfaces;
using TelegramBot.Services;
using TelegramBot.Services.ApiServices;

namespace TelegramBot.Messages
{
    public class MessageCollector
    {
        private readonly IBotService bot;
        private readonly int messageId;
        private readonly long chatId;

        [Obsolete]
        public MessageCollector(long chatId, int messageId)
        {
            bot = new BotService(chatId);
            this.messageId = messageId;
            this.chatId = chatId;
        }

        public async Task SendStartMenu()
        {
            ButtonsGenerator buttonsGenerator = new();

            buttonsGenerator.SetInlineButtons(new List<List<string>>()
            {
                new List<string>{ "Предметы" },
                new List<string>{ "Новости" },
                new List<string>{ "О нас" },
            });

            var usersService = new UsersService();
            var currentUser = await usersService.GetByChatId(chatId);
            if (currentUser?.Role == Data.Models.Role.ADMIN) buttonsGenerator.SetInlineButtons(new List<string>() { "Отправить всем" } );

            await bot.SendMessage("Доброе пожаловать в чат Важной информации.\nЧто бы вы хотели узнать?", buttonsGenerator.GetButtons());
        }

        public async Task EditToStartMenu()
        {
            ButtonsGenerator buttonsGenerator = new();
            var usersService = new UsersService();
            var currentUser = await usersService.GetByChatId(chatId);

            buttonsGenerator.SetInlineButtons(new List<List<string>>()
            {
                new List<string>{ "Предметы" },
                new List<string>{ "Новости" },
                new List<string>{ "О нас" },
        });
            if (currentUser?.Role == Data.Models.Role.ADMIN)
            {
                buttonsGenerator.SetInlineButtons(new List<List<string>>()
            {new List<string> { "Отправить всем" }, });
            }

            await bot.EditMessage("Доброе пожаловать в чат Важной информации.\nЧто бы вы хотели узнать?", buttonsGenerator.GetButtons(), messageId);
        }

        public async Task EditToAboutUsMenu()
        {
            ButtonsGenerator buttonsGenerator = new();
            buttonsGenerator.SetInlineUrlButtons(new List<(string, string)> { ("Наш сайт", AppSettings.FrontRoot) });
            buttonsGenerator.SetInlineButtons(new List<(string, string)> { ("<<Назад", "/start") });

            await bot.EditMessage(MessagesTexts.AboutUs, buttonsGenerator.GetButtons(), messageId);
        }

        public async Task EditToLessonsMenu()
        {
            LessonsService lessonsService = new();
            var lessons = await lessonsService.Get();

            ButtonsGenerator buttonsGenerator = new();

            for (int i = 0; i < lessons.Count; i += 3)
            {
                if (lessons.Count < i + 3)
                {
                    if (lessons.Count - i == 2) buttonsGenerator.SetInlineButtons(new List<string> { lessons[i].Name, lessons[i + 1].Name });
                    if (lessons.Count - i == 1) buttonsGenerator.SetInlineButtons(new List<string> { lessons[i].Name });
                }
                else
                {
                    buttonsGenerator.SetInlineButtons(new List<string> { lessons[i].Name, lessons[i + 1].Name, lessons[i + 2].Name });
                }
            }

            buttonsGenerator.SetInlineButtons(new List<(string, string)> { ("<<Назад", "/start") });

            await bot.EditMessage("Для просмотра детальной информации по предмету, нажмите на кнопку", buttonsGenerator.GetButtons(), messageId);
        }

        public async Task EditToText(string text)
        {
            await bot.EditMessage(text, messageId);
        }

        public async Task UnknownMessage()
        {
            await bot.SendMessage("Пока я не понимаю данное сообщение, но скоро научусь");
        }
    }
}