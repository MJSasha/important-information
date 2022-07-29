using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TelegramBot.Data.Models
{
    public class User
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("login")]
        public string Login { get; set; }
        [JsonPropertyName("password")]
        public Password Password { get; set; }
        [JsonPropertyName("token")]
        public string Token { get; set; }
        [JsonPropertyName("chatId")]
        public long? ChatId { get; set; }
        [JsonPropertyName("role")]
        public Role Role { get; set; }
        [JsonPropertyName("notes")]
        public List<Note> Notes { get; set; }
    }

    public enum Role
    {
        USER,
        ADMIN
    }

    public class Password
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}
