using System;
using System.Text.Json.Serialization;

namespace TelegramBot.Data.ViewModels
{
    public class LessonsAndTimes
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("time")]
        public DateTime? Time { get; set; }    //"HH:mm:ss"

        [JsonPropertyName("lesson")]
        public Lesson Lesson { get; set; }
    }
}
