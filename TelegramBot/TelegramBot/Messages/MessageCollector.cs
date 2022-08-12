using System;
using TelegramBot.Utils;
using System.Threading.Tasks;
using System.Collections.Generic;
using TelegramBot.Data;
using TelegramBot.Data.Models;
using TelegramBot.Data.ViewModels;
using TelegramBot.Interfaces;
using TelegramBot.Services;
using TelegramBot.Services.ApiServices;
using TelegramBot.Utils;

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

            await bot.EditMessage("Доброе пожаловать в чат Важной информации.\nЧто бы вы хотели узнать?", messageId, buttonsGenerator.GetButtons());
        }

        public async Task EditToAboutUsMenu()
        {
            ButtonsGenerator buttonsGenerator = new();
            buttonsGenerator.SetInlineUrlButtons(new List<(string, string)> { ("Наш сайт", AppSettings.FrontRoot) });
            buttonsGenerator.SetGoBackButton();

            await bot.EditMessage(MessagesTexts.AboutUs, messageId, buttonsGenerator.GetButtons());
        }

        public async Task SendWeekNews(int newsShift = 0)
        {
            DateTime weekStartDate = DateTime.Now.AddDays(-(DateTime.Now.DayOfWeek - DayOfWeek.Monday)).AddDays(7 * newsShift);
            DateTime weekEndDate = weekStartDate.AddDays(6);

            var allNewsInSelectedWeek = await GetWeekNews(weekStartDate);
            ButtonsGenerator buttonsGenerator = new();

            await SendNews(allNewsInSelectedWeek);

            if (weekEndDate < DateTime.Now && await CheckAnyNewsBefore(weekEndDate))
            {
                buttonsGenerator.SetInlineButtons(new List<(string, string)> { ("⬅ Предыдущая", $"newsShift:{newsShift - 1}"), ("Следующая ➡", $"newsShift:{newsShift + 1}") });
            }
            else
            {
                if (weekEndDate < DateTime.Now) buttonsGenerator.SetInlineButton(("Следующая ➡", $"newsShift:{newsShift + 1}"));
                else if (await CheckAnyNewsBefore(weekEndDate)) buttonsGenerator.SetInlineButton(("⬅ Предыдущая", $"newsShift:{newsShift - 1}"));
            }
            buttonsGenerator.SetGoBackButton();

            await bot.EditMessage(MessagesTexts.AboutUs, messageId, buttonsGenerator.GetButtons());
        }

        public async Task SendAllNews()
        {
            NewsService newsService = new();
            var allNews = await newsService.Get();

            foreach (var news in allNews)
            {
                await bot.SendMessage($"date time: {news.DateTimeOfCreate}\n" +
                    $"text: {news.Message}\n" +
                    $"pictures: {news.Pictures}");
            }

            ButtonsGenerator buttonsGenerator = new();
            buttonsGenerator.SetInlineButtons(new List<(string, string)> { ("<<Назад", "/start") });
            await bot.SendMessage("На данный момент это все доступные новости, для возвращение в стартовое меню нажмите на кнопку", buttonsGenerator.GetButtons());
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
                    if (lessons.Count - i == 2) buttonsGenerator.SetInlineButtons(new List<(string, string)> { (lessons[i].Name, lessons[i].GetLessonCallback()),
                        (lessons[i + 1].Name, lessons[i + 1].GetLessonCallback()) });
                    if (lessons.Count - i == 1) buttonsGenerator.SetInlineButtons(new List<(string, string)> { (lessons[i].Name, lessons[i].GetLessonCallback()) });
                }
                else
                {
                    buttonsGenerator.SetInlineButtons(new List<(string, string)> { (lessons[i].Name, lessons[i].GetLessonCallback()),
                        (lessons[i + 1].Name, lessons[i + 1].GetLessonCallback()), (lessons[i + 2].Name, lessons[i + 2].GetLessonCallback()) });
                }
            }
        }

        public async Task EditToLesson(int lessonId)
        {
            ButtonsGenerator buttonsGenerator = new();
            buttonsGenerator.SetInlineButtons(new List<(string, string)> { ("<<Назад", "Предметы") });

            await bot.EditMessage(MessagesTexts.AboutUs, messageId, buttonsGenerator.GetButtons());
        }


        public async Task EditToText(string text)
        {
            await bot.EditMessage(text, messageId);
        }

        public async Task UnknownMessage()
        {
            await bot.SendMessage("Пока я не понимаю данное сообщение, но скоро научусь");
        }

        private async Task SendNews(IOrderedEnumerable<News> news)
        {
            foreach (var oneNews in news)
            {
                await bot.SendMessage($"date time: {oneNews.DateTimeOfCreate}\n" +
                    $"text: {oneNews.Message}\n" +
                    $"pictures: {oneNews.Pictures}");
            }
        }
        private async Task<IOrderedEnumerable<News>> GetWeekNews(DateTime weekStartDate)
        {
            NewsService newsService = new();
            return (await newsService.Get(new StartEndTime
            {
                Start = weekStartDate,
                End = weekStartDate.AddDays(7)
            })).OrderBy(n => n.DateTimeOfCreate);
        }
        private async Task<bool> CheckAnyNewsBefore(DateTime date)
        {
            date = date.AddDays(-7);
            NewsService newsService = new();
            var newsBefore = await newsService.Get(new StartEndTime { End = date });
            return newsBefore.Any();
        }
    }
}