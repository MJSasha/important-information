using TelegramBot.Data.ViewModels;

namespace TelegramBot.Utils
{
    public static class Extensions
    {
        public static string GetLessonCallback(this Lesson lesson) => $"lessonId:{lesson.Id}";
    }
}
