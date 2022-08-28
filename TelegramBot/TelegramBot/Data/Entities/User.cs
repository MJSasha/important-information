using Newtonsoft.Json;
using System.Collections.Generic;
using TelegramBot.Data.Definitions;

namespace TelegramBot.Data.Entities
{
    public class User
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("login")]
        public string Login { get; set; }

        [JsonProperty("password")]
        public Password Password { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("chatId")]
        public long ChatId { get; set; }

        [JsonProperty("role")]
        public Role Role { get; set; }

        [JsonProperty("notes")]
        public List<Note> Notes { get; set; }
    }
}
