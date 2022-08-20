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
        //public static string GetDayId(this Day day) => $"dayId:{day.Id}";
        public static string GetLessonCard(this Lesson lesson) => $"📚 {lesson.Name}\n" +
            $"Преподователь: {lesson.Teacher}.\n" +
            $"Информация: {lesson.Information}";
        public static string GetDayCard(this Day day)
        {
            string timetable = "";
            foreach (var item in day.LessonsAndTimes)
            {
                timetable += $"{item.Lesson.Name} {item.Time}\n";
            }
                return $"🗓{ day.Date}\n" +
                 $"{day.Information}\n\n" +
                 $"🕑 Расписание занятий:\n{timetable}";
        }
        public static string GetNewsByDate(this News oneNews) => $"🕓{oneNews.DateTimeOfCreate}\n" +
            $"‼{oneNews.Message}\n\n" +
            $"{oneNews.Pictures}";
        public static void SetGoBackButton(this ButtonsGenerator buttonsGenerator, string callback = "/start") => buttonsGenerator.SetInlineButtons(new List<(string, string)> { ("↪ Назад", callback) });
    }
}