using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace TelegramBot.Data.Models
{
    public class News
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; }
        [JsonPropertyName("needToSend")]
        public bool NeedToSend { get; set; }
    }
}
