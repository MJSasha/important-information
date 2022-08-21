using System;
using System.Text.Json.Serialization;
using TelegramBot.Utils;
using JsonConverterAttribute = Newtonsoft.Json.JsonConverterAttribute;

namespace TelegramBot.Data.ViewModels
{
    public class LessonsAndTimes
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("time")]
        [JsonConverter(typeof(CustomDateTimeConverter), "HH:mm:ss")]
        public DateTime? Time { get; set; }

        [JsonPropertyName("lesson")]
        public Lesson Lesson { get; set; }
    }
}
