using System;
using System.Text.Json.Serialization;
using TelegramBot.Utils;

namespace TelegramBot.Data.Models
{
    public class News
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("dateTimeOfCreate")]
        [Newtonsoft.Json.JsonConverter(typeof(CustomDateTimeConverter), "yyyy-MM-dd HH:mm:ss")]
        public DateTime DateTimeOfCreate { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("pictures")]
        public string Pictures { get; set; }

        [JsonPropertyName("needToSend")]
        public bool NeedToSend { get; set; }
    }
}
