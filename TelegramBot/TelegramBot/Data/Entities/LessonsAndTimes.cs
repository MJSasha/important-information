using Newtonsoft.Json;
using System;
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

        [JsonProperty("lesson")]
        public Lesson Lesson { get; set; }
    }
}
