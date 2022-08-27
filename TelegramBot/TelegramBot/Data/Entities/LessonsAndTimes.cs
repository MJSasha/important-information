using Newtonsoft.Json;
using System;
using TelegramBot.Data.Definitions;
using TelegramBot.Utils;

namespace TelegramBot.Data.ViewModels
{
    public class LessonsAndTimes
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("time")]
        [JsonConverter(typeof(CustomDateTimeConverter), "HH:mm:ss")]
        public DateTime? Time { get; set; }

        [JsonProperty("type")]
        public LessonType type;

        [JsonProperty("lesson")]
        public Lesson Lesson { get; set; }
    }
}
