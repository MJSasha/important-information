using System.Text.Json.Serialization;

namespace TelegramBot.Data.ViewModels
{
    public class Lesson
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("teacher")]
        public string Teacher { get; set; }
        [JsonPropertyName("information")]
        public string Information { get; set; }
    }
}
