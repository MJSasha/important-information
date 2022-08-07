using System;
using System.Text.Json.Serialization;

namespace TelegramBot.Data.Models
{
    public class Note
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("day")]
        public DateTime Day { get; set; }

        [JsonPropertyName("user")]
        public User User { get; set; }
    }
}
