using System.Text.Json.Serialization;

namespace TelegramBot.Data.Models
{
    public class News
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("pictures")]
        public string Pictures { get; set; }

        [JsonPropertyName("needToSend")]
        public bool NeedToSend { get; set; }
    }
}
