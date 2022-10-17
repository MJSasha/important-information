using ImpInfCommon.Data.Definitions;
using ImpInfCommon.Data.Models;
using ImpInfCommon.Data.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Data;
using TelegramBot.Handlers;
using TelegramBot.Services;
using TelegramBot.Services.ApiServices;
using TelegramBot.Utils;
using TgBotLib.Interfaces;
using TgBotLib.Utils;

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
            await bot.SendMessage(Texts.StartMenu, await GenerateButtonsForStartMenu());
        }

        public async Task EditToStartMenu()
        {
            await bot.EditMessage(Texts.StartMenu, messageId, await GenerateButtonsForStartMenu());
        }

        public async Task EditToAdminPanel()
        {
            ButtonsGenerator buttonsGenerator = new();
            buttonsGenerator.SetInlineButtons("Создать рассылку");
            buttonsGenerator.SetInlineButtons("Сведения о пользователях");

            buttonsGenerator.SetGoBackButton();

            await bot.EditMessage(Texts.AdminPanel, messageId, buttonsGenerator.GetButtons());
        }

        public async Task EditToUsersData()
        {
            ButtonsGenerator buttonsGenerator = new();
            UsersService usersService = new();
            var users = await usersService.Get();
            var message = "";
            foreach (var item in users)
            {
                message += $"{item.GetUserCard()}\n\n";
            }
            buttonsGenerator.SetGoBackButton("Панель администратора");
            await bot.EditMessage(message, messageId, buttonsGenerator.GetButtons());
        }

        public async Task ChangeUserRole(int selectedUserChatId)
        {
            UsersService usersService = new();
            var currentUser = await usersService.GetByChatId(chatId);
            if (currentUser?.Role == Role.ADMIN && chatId != selectedUserChatId)
            {
                var changedUser = await usersService.GetByChatId(selectedUserChatId);
                if (changedUser != null)
                {
                    ButtonsGenerator buttonsGenerator = new();
                    changedUser.Role = changedUser?.Role == Role.ADMIN ? Role.USER : Role.ADMIN;
                    await usersService.Update(changedUser.Id, changedUser);
                    buttonsGenerator.SetGoBackButton("Сведения о пользователях");
                    await bot.SendMessage(Texts.ChangeOfRole, buttonsGenerator.GetButtons());
                }
                else
                {
                    await bot.SendMessage(Texts.NonExistentUser);
                }
            }
            else await bot.SendMessage(Texts.NoRights);
        }

        public async Task EditToAboutUsMenu()
        {
            ButtonsGenerator buttonsGenerator = new();
            buttonsGenerator.SetInlineUrlButtons(("Наш сайт", AppSettings.FrontRoot));
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
                    if (lessons.Count - i == 2) buttonsGenerator.SetInlineButtons((lessons[i].Name, lessons[i].GetLessonCallback()),
                        (lessons[i + 1].Name, lessons[i + 1].GetLessonCallback()));
                    if (lessons.Count - i == 1) buttonsGenerator.SetInlineButtons((lessons[i].Name, lessons[i].GetLessonCallback()));
                }
                else
                {
                    buttonsGenerator.SetInlineButtons((lessons[i].Name, lessons[i].GetLessonCallback()),
                        (lessons[i + 1].Name, lessons[i + 1].GetLessonCallback()), (lessons[i + 2].Name, lessons[i + 2].GetLessonCallback()));
                }
            }

            buttonsGenerator.SetGoBackButton();

            await bot.EditMessage(Texts.DetailLessonInfo, messageId, buttonsGenerator.GetButtons());
        }

        public async Task EditToWeekNews(int newsShift = 0)
        {
            DateTime weekStartDate = DateTime.Now.AddDays(-(DateTime.Now.DayOfWeek - DayOfWeek.Monday)).AddDays(7 * newsShift);
            DateTime weekEndDate = weekStartDate.AddDays(6);

            var allNewsInSelectedWeek = await GetWeekNews(weekStartDate);
            ButtonsGenerator buttonsGenerator = new();


            if (weekEndDate < DateTime.Now && await CheckAnyNewsBefore(weekEndDate))
            {
                buttonsGenerator.SetInlineButtons(("⬅ Предыдущая", $"newsShift:{newsShift - 1}"), ("Следующая ➡", $"newsShift:{newsShift + 1}"));
            }
            else
            {
                if (weekEndDate < DateTime.Now) buttonsGenerator.SetInlineButtons(("Следующая ➡", $"newsShift:{newsShift + 1}"));
                else if (await CheckAnyNewsBefore(weekEndDate)) buttonsGenerator.SetInlineButtons(("⬅ Предыдущая", $"newsShift:{newsShift - 1}"));
            }
            buttonsGenerator.SetGoBackButton();

            await SendNews(allNewsInSelectedWeek, buttonsGenerator.GetButtons(), string.Format(Texts.NextWeek, weekStartDate.ToString("dd-MM-yyyy"), weekEndDate.ToString("dd-MM-yyyy")));
        }

        public async Task EditToCalendar(int monthShift = 0)
        {
            ButtonsGenerator buttonsGenerator = new();
            var monthStartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(monthShift);

            for (int k = 0; k < 4; k++)
            {
                List<(string, string)> buttonsLine = new();
                for (int i = 0; i < 7; i++) buttonsLine.Add((monthStartDate.AddDays(i).DayOfWeek.ToRusDay() + monthStartDate.AddDays(i).Day.Above(), monthStartDate.AddDays(i).GetDayCallback()));
                buttonsGenerator.SetInlineButtons(buttonsLine.ToArray());
                monthStartDate = monthStartDate.AddDays(7);
            }
            monthStartDate = monthStartDate.AddDays(-1);

            List<(string, string)> completedButtonsLine = new();
            while (monthStartDate.Day < DateTime.DaysInMonth(monthStartDate.Year, monthStartDate.Month))
            {
                monthStartDate = monthStartDate.AddDays(1);
                completedButtonsLine.Add((monthStartDate.DayOfWeek.ToRusDay() + monthStartDate.Day.Above(), monthStartDate.GetDayCallback()));
            }

            buttonsGenerator.SetInlineButtons(completedButtonsLine.ToArray());
            await SetPaginationButtonsForDays(buttonsGenerator, monthShift);
            buttonsGenerator.SetGoBackButton();
            await bot.EditMessage($"*{monthStartDate.Month.GetMonthName()} {monthStartDate.Year}-го года*\nДля просмотра детальной информации по дате, нажмите на кнопку", messageId, buttonsGenerator.GetButtons());

        }
        #endregion

        public async Task TryToStartRegistration()
        {
            UsersService usersService = new UsersService();
            var user = await usersService.GetByChatId(chatId);
            if (user == null)
            {
                DistributionService.BusyUsersIdAndService.Add(chatId, new RegistrationHandler(chatId));
            }
            else
            {
                await bot.SendMessage("Ты уже зареган");
            }
        }
        public async Task EditToLesson(int lessonId)
        {
            ButtonsGenerator buttonsGenerator = new();
            UsersService usersService = new();
            var user = await usersService.GetByChatId(chatId);

            buttonsGenerator.SetInlineButtons(("Новости по предмету", $"getNewsForLes{lessonId}I{messageId}"));
            if (user.Role == Role.ADMIN) buttonsGenerator.SetInlineButtons(("Редактировать", $"redactNews{lessonId}"));
            buttonsGenerator.SetGoBackButton("Предметы");

            LessonsService lessonsService = new();
            var lesson = await lessonsService.Get(lessonId);

            await bot.EditMessage(lesson.GetLessonCard(), messageId, buttonsGenerator.GetButtons());
        }
        public async Task EditLesson(int lessonId)
        {
            ButtonsGenerator buttonsGenerator = new();
            LessonsService lessonsService = new();
            var lesson = await lessonsService.Get(lessonId);

            buttonsGenerator.SetInlineButtons(new[] { ("Название", $"editName{lessonId}") },
                                              new[] {("Преподавателя", $"editTeacher{lessonId}"), ("Информацию", $"editInformation{lessonId}") });

            buttonsGenerator.SetGoBackButton(lesson.GetLessonCallback());

            await bot.EditMessage(lesson.GetLessonCard() + "\n\n_Выберите, что будете редактировать_", messageId, buttonsGenerator.GetButtons());
        }
        public async Task EditToDay(DateTime chosenDay)
        {
            DaysServices daysServices = new();
            var day = await daysServices.Get(new DateTimeWrap() { DateTime = chosenDay });

            ButtonsGenerator buttonsGenerator = new();
            buttonsGenerator.SetInlineButtons(("Добавить заметку", $"addNote{chosenDay}"));
            buttonsGenerator.SetGoBackButton("Календарь");

            if (day != null) await bot.EditMessage(day.GetDayCard(), messageId, buttonsGenerator.GetButtons());
            else await bot.EditMessage($"Отсутствует инормация по дате {chosenDay:dd-MM-yyyy}", messageId, buttonsGenerator.GetButtons());
        }
        public async Task EditDay(DateTime chosenDay)
        {
            DaysServices daysServices = new();
            var day = await daysServices.Get(new DateTimeWrap() { DateTime= chosenDay });
            var dayId = day.Id;

            DistributionService.BusyUsersIdAndService.Add(chatId, new NoteHandler(chatId, day, dayId));
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
            await bot.SendMessage(Texts.UnknownMessage);
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
                Start = weekStartDate.AddDays(-1),
                End = weekStartDate.AddDays(6)
            })).OrderBy(n => n.DateTimeOfCreate);
        }
        private async Task<bool> CheckAnyNewsBefore(DateTime date)
        {
            date = date.AddDays(-7);
            NewsService newsService = new();
            return await newsService.CheckNewsBefore(date);
        }
        private async Task SetPaginationButtonsForDays(ButtonsGenerator buttonsGenerator, int monthShift)
        {
            DaysServices daysServices = new();
            List<(string, string)> paginationButtons = new();
            var date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(monthShift);

            if (await daysServices.AnyBefore(new DateTimeWrap { DateTime = date }))
            {
                paginationButtons.Add(("⬅ Предыдущий", $"monthShift:{monthShift - 1}"));
            }
            if (await daysServices.AnyAfter(new DateTimeWrap { DateTime = date.AddMonths(1) }))
            {
                paginationButtons.Add(("Следующий ➡", $"monthShift:{monthShift + 1}"));
            }

            buttonsGenerator.SetInlineButtons(paginationButtons.ToArray());
        }
        private async Task<IReplyMarkup> GenerateButtonsForStartMenu()
        {
            ButtonsGenerator buttonsGenerator = new();
            buttonsGenerator.SetInlineButtons(new[] { "Предметы" },
                                              new[] { "Новости" },
                                              new[] { "Календарь" },
                                              new[] { "О нас" });

            var usersService = new UsersService();
            var currentUser = await usersService.GetByChatId(chatId);
            if (currentUser?.Role == Role.ADMIN) buttonsGenerator.SetInlineButtons("Панель администратора");
            return buttonsGenerator.GetButtons();
        }
        #endregion
    }
}