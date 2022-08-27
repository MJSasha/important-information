using System;
using System.Collections.Generic;
using TelegramBot.Data.Models;
using TelegramBot.Data.ViewModels;
using TelegramBot.Services;

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

        public static string GetDayCallback(this DateTime date) => $"dayDate:{date:DD-MM-yyyy}";
        public static string GetLessonCallback(this Lesson lesson) => $"lessonId:{lesson.Id}";

        public static string GetLessonCard(this Lesson lesson) =>
            lesson.Information != null ? $"📚 {lesson.Name}\n Преподователь: {lesson.Teacher}\n " +
            $"Информация: {lesson.Information}" : $"📚 {lesson.Name}" +
            $"Преподаватель: {lesson.Teacher}.";
        public static string GetDayCard(this Day day)
        {
            string schedule = "";
            foreach (var item in day.LessonsAndTimes)
            {
                schedule += $"{item.Lesson.Name} - {item.Time}\n";
            }
                return $"🗓{ day.Date}\n" +
                 $"{day.Information}\n\n" +
                 $"🕑 Расписание занятий:\n{schedule}";
        }
        public static string GetNewsCard(this News oneNews) => oneNews.Message != null ? $"🕓{oneNews.DateTimeOfCreate}\n\n‼{oneNews.Message}" : $"🕓{oneNews.DateTimeOfCreate}" ;

        public static void SetGoBackButton(this ButtonsGenerator buttonsGenerator, string callback = "/start") => buttonsGenerator.SetInlineButtons(("↪ Назад", callback));
    }
}