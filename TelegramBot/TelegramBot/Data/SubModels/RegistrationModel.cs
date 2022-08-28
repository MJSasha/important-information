using Newtonsoft.Json;

namespace TelegramBot.Data.SubModels
{
    public class RegistrationModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("login")]
        public string Login { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
