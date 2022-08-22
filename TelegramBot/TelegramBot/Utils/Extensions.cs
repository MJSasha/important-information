using System;
using System.Collections.Generic;
using TelegramBot.Data.Models;
using TelegramBot.Data.ViewModels;
using TelegramBot.Services;

namespace TelegramBot.Utils
{
    public static class Extensions
    {

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
        public static string GetNewsCard(this News oneNews) => 
            oneNews.Message != null ? $"🕓{oneNews.DateTimeOfCreate}\n\n‼{oneNews.Message}" : $"🕓{oneNews.DateTimeOfCreate}" ;
        public static void SetGoBackButton(this ButtonsGenerator buttonsGenerator, string callback = "/start") => buttonsGenerator.SetInlineButtons(new List<(string, string)> { ("↪ Назад", callback) });
    }
}