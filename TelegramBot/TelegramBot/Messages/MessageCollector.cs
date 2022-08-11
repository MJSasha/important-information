using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        [Obsolete]
        public MessageCollector(long chatId, int messageId)
        {
            bot = new BotService(chatId);
            this.messageId = messageId;
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

            await bot.SendMessage("Доброе пожаловать в чат Важной информации.\nЧто бы вы хотели узнать?", buttonsGenerator.GetButtons());
        }

        public async Task EditToStartMenu()
        {
            ButtonsGenerator buttonsGenerator = new();
            buttonsGenerator.SetInlineButtons(new List<List<string>>()
            {
                new List<string>{ "Предметы" },
                new List<string>{ "Новости" },
                new List<string>{ "О нас" },
            });

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
            DateTime currentWeekStartDate = DateTime.Now.AddDays(-(DateTime.Now.DayOfWeek - DayOfWeek.Monday));
            NewsService newsService = new();
            var allNewsInSelectedWeek = (await newsService.Get(new StartEndTime
            {
                Start = currentWeekStartDate.AddDays(7 * newsShift),
                End = currentWeekStartDate.AddDays(7 + 7 * newsShift)
            })).OrderBy(n => n.DateTimeOfCreate);

            ButtonsGenerator buttonsGenerator = new();

            if (allNewsInSelectedWeek.Any())
            {
                await SendNews(allNewsInSelectedWeek);
                SetPaginationAndGoBackButtons(buttonsGenerator, newsShift);
                await bot.SendMessage($"Новости, созданные в промежуток С {currentWeekStartDate.AddDays(7 * newsShift):dd-MM-yyyy} ДО {currentWeekStartDate.AddDays(6 + 7 * newsShift):dd-MM-yyyy}\n" +
                    $"Для перехода к другой неделе нажмите на кнопку", buttonsGenerator.GetButtons());
            }
            else
            {
                newsShift += newsShift > 0 ? -1 : 1;
                SetPaginationAndGoBackButtons(buttonsGenerator, newsShift);
                await bot.EditMessage("Дальше новостей нет, туда не ходи!", messageId, buttonsGenerator.GetButtons());
            }
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

            buttonsGenerator.SetGoBackButton();

            await bot.EditMessage("Для просмотра детальной информации по предмету, нажмите на кнопку", messageId, buttonsGenerator.GetButtons());
        }

        public async Task EditToLesson(int lessonId)
        {
            ButtonsGenerator buttonsGenerator = new();
            buttonsGenerator.SetGoBackButton("Предметы");

            LessonsService lessonsService = new();
            var lesson = await lessonsService.Get(lessonId);

            await bot.EditMessage($"id: {lesson.Id}\n" +
                $"name: {lesson.Name}\n" +
                $"teacher: {lesson.Teacher}\n" +
                $"information: {lesson.Information}", messageId, buttonsGenerator.GetButtons());
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
        private void SetPaginationAndGoBackButtons(ButtonsGenerator buttonsGenerator, int newsShift)
        {
            buttonsGenerator.SetInlineButtons(new List<(string, string)> { ("⬅ Предыдущая", $"newsShift:{newsShift - 1}"), ("Следующая ➡", $"newsShift:{newsShift + 1}") });
            buttonsGenerator.SetGoBackButton();
        }
    }
}