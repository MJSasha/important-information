using System.Text.Json.Serialization;

namespace TelegramBot.Data.Entities
{
    public class Password
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}
