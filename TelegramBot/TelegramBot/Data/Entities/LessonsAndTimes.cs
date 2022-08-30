using Newtonsoft.Json;
using System;
using TelegramBot.Data.Definitions;
using TelegramBot.Utils;

namespace TelegramBot.Data.Entities
{
    public class LessonsAndTimes
    {
        public int Id { get; set; }
        [JsonConverter(typeof(CustomDateTimeConverter), "HH:mm:ss")]
        public DateTime? Time { get; set; }
        public LessonType Type { get; set; }
        public Lesson Lesson { get; set; }
    }
}
