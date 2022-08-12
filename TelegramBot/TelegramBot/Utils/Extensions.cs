using System.Collections.Generic;
using TelegramBot.Data.ViewModels;
using TelegramBot.Services;

namespace TelegramBot.Utils
{
    public static class Extensions
    {
        public static string GetLessonCallback(this Lesson lesson) => $"lessonId:{lesson.Id}";
        public static void SetGoBackButton(this ButtonsGenerator buttonsGenerator, string callback = "/start") => buttonsGenerator.SetInlineButtons(new List<(string, string)> { ("↪ Назад", callback) });
    }
}