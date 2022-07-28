using System.Text.Json.Serialization;

namespace TelegramBot.Data.ViewModels
{
    public class RegistrationModel
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("login")]
        public string Login { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
