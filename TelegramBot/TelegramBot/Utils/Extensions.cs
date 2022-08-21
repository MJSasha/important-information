using System.Collections.Generic;
using TelegramBot.Data.Models;
using TelegramBot.Data.ViewModels;
using TelegramBot.Services;

namespace TelegramBot.Utils
{
    public static class Extensions
    {
        public static string GetLessonCallback(this Lesson lesson) => $"lessonId:{lesson.Id}";
        public static string GetNewsCard(this News news)
        {
            var textToReturn = $"date time: {news.DateTimeOfCreate}\ntext: {news.Message}\n";
            textToReturn += news.Lesson == null ? "" : $"lesson: {news.Lesson.Name}\n";
            return textToReturn;
        }
        public static void SetGoBackButton(this ButtonsGenerator buttonsGenerator, string callback = "/start") => buttonsGenerator.SetInlineButtons(new List<(string, string)> { ("↪ Назад", callback) });
    }
}