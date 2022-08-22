using Newtonsoft.Json;

namespace TelegramBot.Data.ViewModels
{
    public class Lesson
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("teacher")]
        public string Teacher { get; set; }

        [JsonProperty("information")]
        public string Information { get; set; }
    }
}
