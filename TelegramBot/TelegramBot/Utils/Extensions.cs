using System.Collections.Generic;
using TelegramBot.Data.Entities;
using TgBotLib.Utils;

namespace TelegramBot.Utils
{
    public static class Extensions
    {

        public static string GetLessonCallback(this Lesson lesson) => $"lessonId:{lesson.Id}";

        public static string GetLessonCard(this Lesson lesson)
        {
            var card = $"📚 {lesson.Name}\nПреподователь: {lesson.Teacher}\n";
            card += string.IsNullOrWhiteSpace(lesson.Information) ? "" : $"Информация: {lesson.Information}";
            return card;
        }

        public static string GetDayCard(this Day day)
        {
            string schedule = "";
            foreach (var item in day.LessonsAndTimes)
            {
                schedule += $"{item.Lesson.Name} - {item.Time}\n";
            }
            return $"🗓{day.Date}\n" +
                $"{day.Information}\n\n" +
                $"🕑 Расписание занятий:\n{schedule}";
        }

        public static string GetNewsCard(this News oneNews)
        {
            var card = $"🕓{oneNews.DateTimeOfCreate}\n";
            card += string.IsNullOrWhiteSpace(oneNews.Message) ? "" : $"‼️ {oneNews.Message}";
            return card;
        }

        public static void SetGoBackButton(this ButtonsGenerator buttonsGenerator, string callback = "/start") => buttonsGenerator.SetInlineButtons(new List<(string, string)> { ("↪ Назад", callback) });
    }
}