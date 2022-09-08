using System;
using TelegramBot.Data.Entities;
using TgBotLib.Utils;

namespace TelegramBot.Utils
{
    public static class Extensions
    {
        public static string Above(this int number)
        {
            var stringNumber = number.ToString();
            string aboveNumber = "";
            for (int i = 0; i < stringNumber.Length; i++)
            {
                aboveNumber += stringNumber[i] switch
                {
                    '1' => "¹",
                    '2' => "²",
                    '3' => "³",
                    '4' => "⁴",
                    '5' => "⁵",
                    '6' => "⁶",
                    '7' => "⁷",
                    '8' => "⁸",
                    '9' => "⁹",
                    '0' => "⁰",
                    _ => "",
                };
            }
            return aboveNumber;
        }
        public static string ToRusDay(this DayOfWeek day)
        {
            return day switch
            {
                DayOfWeek.Monday => "Пн",
                DayOfWeek.Tuesday => "Вт",
                DayOfWeek.Wednesday => "Ср",
                DayOfWeek.Thursday => "Чт",
                DayOfWeek.Friday => "Пт",
                DayOfWeek.Saturday => "Сб",
                DayOfWeek.Sunday => "Вс",
                _ => "",
            };
        }

        public static string GetDayCallback(this DateTime date) => $"dayDate:{date:yyyy-MM-dd}";
        public static string GetLessonCallback(this Lesson lesson) => $"lessonId:{lesson.Id}";

        public static string GetLessonCard(this Lesson lesson)
        {
            return $"📚 {lesson.Name}\nПреподователь: {lesson.Teacher}\n" +
                $"{(string.IsNullOrWhiteSpace(lesson.Information) ? "" : $"Информация: {lesson.Information}")}";
        }

        public static string GetDayCard(this Day day)
        {
            string schedule = "";
            foreach (var item in day.LessonsAndTimes)
            {
                schedule += $"•\t{item.Time:HH:mm} - {item.Lesson.Name} ({item.Type.GetName()})\n";
            }
            return $"🗓{day.Date:dd-MM-yyyy}\n" +
                $"{(string.IsNullOrWhiteSpace(day.Information) ? "" : $"\n{day.Information}\n")}" +
                $"\n{(string.IsNullOrWhiteSpace(schedule) ? "‼️ Выходной ‼️" : $"Расписание:\n{schedule}")}";
        }

        public static string GetNewsCard(this News oneNews)
        {
            return $"🕓{oneNews.DateTimeOfCreate}\n" +
                $"{(string.IsNullOrWhiteSpace(oneNews.Message) ? "" : $"‼️ {oneNews.Message}")}";
        }

        public static void SetGoBackButton(this ButtonsGenerator buttonsGenerator, string callback = "/start") => buttonsGenerator.SetInlineButtons(("↪ Назад", callback));
    }
}