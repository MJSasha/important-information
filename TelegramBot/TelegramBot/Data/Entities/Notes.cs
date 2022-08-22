using Newtonsoft.Json;

namespace TelegramBot.Data.Models
{
    public class Note
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("day")]
        public Day Day { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }
    }
}
