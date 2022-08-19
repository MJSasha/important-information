using System.Collections.Generic;
using TelegramBot.Data.Models;
using TelegramBot.Data.ViewModels;
using TelegramBot.Services;

namespace TelegramBot.Utils
{
    public static class Extensions
    {
        public static string GetLessonCallback(this Lesson lesson) => $"lessonId:{lesson.Id}";
        public static string GetLessonInfo(this Lesson lesson) => $"📚 {lesson.Name}\n" +
            $"Преподователь: {lesson.Teacher}.\n" +
            $"Информация: {lesson.Information}";
        public static string GetDayInfo(this Day day) => $"📅 {day.Date}\n" +
            $"{day.Information}\n\n" +
            $"🕑 Расписание занятий:{day.LessonsAndTimes}\n\n" +
            $"📝 Заметки:{day.CurrentUserNote}";
        public static string GetNewsInfo(this News oneNews) => $"🕓{oneNews.DateTimeOfCreate}\n" +
            $"{oneNews.Message}\n\n" +
            $"‼{oneNews.NeedToSend}" +
            $"{oneNews.Pictures}";
        public static void SetGoBackButton(this ButtonsGenerator buttonsGenerator, string callback = "/start") => buttonsGenerator.SetInlineButtons(new List<(string, string)> { ("↪ Назад", callback) });
    }
}