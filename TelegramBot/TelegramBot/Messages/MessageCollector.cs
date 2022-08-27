using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Data;
using TelegramBot.Data.Definitions;
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

        public MessageCollector(long chatId, int messageId)
        {
            bot = new BotService(chatId);
            this.messageId = messageId;
            this.chatId = chatId;
        }

        #region Menus
        public async Task SendStartMenu()
        {
            ButtonsGenerator buttonsGenerator = new();
            buttonsGenerator.SetInlineButtons(
                new List<string>{ "Предметы" },
                new List<string>{ "Новости" },
                new List<string>{ "Календарь" },
                new List<string>{ "О нас" });

            var usersService = new UsersService();
            var currentUser = await usersService.GetByChatId(chatId);
            if (currentUser?.Role == Role.ADMIN) buttonsGenerator.SetInlineButtons("Отправить всем");

            await bot.SendMessage(Texts.StartMenu, buttonsGenerator.GetButtons());
        }

        public async Task EditToStartMenu()
        {
            ButtonsGenerator buttonsGenerator = new();
            buttonsGenerator.SetInlineButtons(
                new List<string>{ "Предметы" },
                new List<string>{ "Новости" },
                new List<string>{ "Календарь" },
                new List<string>{ "О нас" });

            var usersService = new UsersService();
            var currentUser = await usersService.GetByChatId(chatId);
            if (currentUser?.Role == Role.ADMIN) buttonsGenerator.SetInlineButtons("Отправить всем");

            await bot.EditMessage(Texts.StartMenu, messageId, buttonsGenerator.GetButtons());
        }

        public async Task EditToAboutUsMenu()
        {
            ButtonsGenerator buttonsGenerator = new();
            buttonsGenerator.SetInlineUrlButtons(new List<(string, string)> { ("Наш сайт", AppSettings.FrontRoot) });
            buttonsGenerator.SetGoBackButton();

            await bot.EditMessage(Texts.AboutUs, messageId, buttonsGenerator.GetButtons());
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

        public async Task EditToDateMenu()
        {

            ButtonsGenerator buttonsGenerator = new();
            DaysServices daysServices = new();
            var days = await daysServices.Get();

            var year = DateTime.Now.Year;
            var month = DateTime.Now.Month;
            var startDay = new DateTime(year, month, 1);
            startDay = startDay.AddMonths(2);
            var endDay = startDay.AddMonths(1);
            string temp;
            int tempi;
            int k = 0;
            string[] tempweek = { "Вс", "Пн", "Вт", "Ср", "Чт", "Пт", "Сб" }; //Sunday first
            int[] keys = { 0, 1, 2, 3, 4, 5, 6 };
            string[] tempweekeng = { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };

            

            for (int i = 0; i < tempweekeng.Length; i++)
            {
                if (tempweekeng[i] == startDay.DayOfWeek.ToString())
                {
                    for (int j = 0; j < tempweek.Length - 1; j++)
                    {
                        temp = tempweek[j];
                        tempweek[j] = tempweek[i];
                        tempweek[i] = temp;

                        tempi = keys[j];
                        keys[j] = keys[i];
                        keys[i] = tempi;
                        Array.Sort(keys, tempweek, j + 1, keys.Length - j - 1);
                        if (i == tempweek.Length - 1)
                        {
                            Array.Sort(keys, tempweek, j + 1, keys.Length - j - 2);
                            break;
                        }
                        else i++;
                    }
                    

                    break;
                }
            }

            while (startDay.Month != endDay.Month)
            {


                if (startDay.Day == 29)
                {
                    for (var endMounth = startDay; endMounth.Month != endDay.Month; endMounth = endMounth.AddDays(1))
                    {
                        k++;
                    }

                    if (k == 1) buttonsGenerator.SetInlineButtons(new List<(string, string)> { ($"{tempweek[0]}{startDay.Day}", days[0].GetDayCard()) });
                    if (k == 2) buttonsGenerator.SetInlineButtons(new List<string> { $"{tempweek[0]}{startDay.Day}", $"{tempweek[1]}{startDay.Day + 1}" });
                    if (k == 3) buttonsGenerator.SetInlineButtons(new List<string> { $"{tempweek[0]}{startDay.Day}", $"{tempweek[1]}{startDay.Day + 1}", $"{tempweek[2]}{startDay.Day + 2}" });
                }
                else
                {
                    buttonsGenerator.SetInlineButtons(new List<string>
                    {
                        $"{tempweek[0]}{startDay.Day}", $"{tempweek[1]}{startDay.Day + 1}" , $"{tempweek[2]}{startDay.Day + 2}" , $"{tempweek[3]}{startDay.Day + 3}" , $"{tempweek[4]}{startDay.Day + 4}" , $"{tempweek[5]}{startDay.Day + 5}", $"{tempweek[6]}{startDay.Day + 6}"
                    });
                }
                startDay = startDay.AddDays(7);
            }


            buttonsGenerator.SetGoBackButton();
            await bot.EditMessage("Для просмотра детальной информации по дате, нажмите на кнопку", messageId, buttonsGenerator.GetButtons());

        }
        public async Task EditToWeekNews(int newsShift = 0)
        {
            DateTime weekStartDate = DateTime.Now.AddDays(-(DateTime.Now.DayOfWeek - DayOfWeek.Monday)).AddDays(7 * newsShift);
            DateTime weekEndDate = weekStartDate.AddDays(6);

            var allNewsInSelectedWeek = await GetWeekNews(weekStartDate);
            ButtonsGenerator buttonsGenerator = new();


            if (weekEndDate < DateTime.Now && await CheckAnyNewsBefore(weekEndDate))
            {
                buttonsGenerator.SetInlineButtons(new List<(string, string)> { ("⬅ Предыдущая", $"newsShift:{newsShift - 1}"), ("Следующая ➡", $"newsShift:{newsShift + 1}") });
            }
            else
            {
                if (weekEndDate < DateTime.Now) buttonsGenerator.SetInlineButtons(("Следующая ➡", $"newsShift:{newsShift + 1}"));
                else if (await CheckAnyNewsBefore(weekEndDate)) buttonsGenerator.SetInlineButtons(("⬅ Предыдущая", $"newsShift:{newsShift - 1}"));
            }
            buttonsGenerator.SetGoBackButton();

            await SendNews(allNewsInSelectedWeek, buttonsGenerator.GetButtons(), $"Новости, созданные в промежуток С {weekStartDate:dd-MM-yyyy} ДО {weekEndDate:dd-MM-yyyy}\n" +
                    $"Для перехода к другой неделе нажмите на кнопку");
        }
        #endregion


        public async Task EditToLesson(int lessonId)
        {
            ButtonsGenerator buttonsGenerator = new();
            buttonsGenerator.SetInlineButtons(("Новости по предмету", $"getNewsForLes{lessonId}I{messageId}"));
            buttonsGenerator.SetGoBackButton("Предметы");

            LessonsService lessonsService = new();
            var lesson = await lessonsService.Get(lessonId);

            await bot.EditMessage(lesson.GetLessonCard(), messageId, buttonsGenerator.GetButtons());
        }

        public async Task SendNewsForLesson(int lessonId, int previewMessageId)
        {
            await bot.DeleteMessage(messageId);
            await bot.DeleteMessage(previewMessageId);

            NewsService newsService = new();
            var news = await newsService.GetByLessonId(lessonId);

            ButtonsGenerator buttonsGenerator = new();
            buttonsGenerator.SetGoBackButton($"lessonId:{lessonId}");

            await BotService.SendNews(news, new List<long> { chatId }, buttonsGenerator.GetButtons());
        }

        public async Task SendDetailedNews(int newsId, int previewMessageId)
        {
            await bot.DeleteMessage(messageId);
            await bot.DeleteMessage(previewMessageId);

            NewsService newsService = new();
            var news = await newsService.Get(newsId);
            ButtonsGenerator buttonsGenerator = new();
            buttonsGenerator.SetGoBackButton("Новости");

            await BotService.SendNews(news, new List<long> { chatId }, buttonsGenerator.GetButtons());
        }

        public async Task UnknownMessage()
        {
            await bot.SendMessage("Пока я не понимаю данное сообщение, но скоро научусь");
        }

        #region Utils
        private async Task SendNews(IOrderedEnumerable<News> news, IReplyMarkup buttons = null, string caption = null)
        {
            string outputString = "";

            foreach (var oneNews in news)
            {
                outputString += oneNews.GetNewsCard();

                outputString += string.IsNullOrWhiteSpace(oneNews.Pictures) ? "\n\n" : $"Новость с картинками. Для просмотра картинок нажмите /news{oneNews.Id}I{messageId}\n\n";
            }

            await bot.EditMessage(outputString + caption, messageId, buttons);
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
            return await newsService.CheckNewsBefore(date);
        }
        #endregion
    }
}