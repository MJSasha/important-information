using Newtonsoft.Json;

namespace TelegramBot.Data.Entities
{
    public class Password
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
