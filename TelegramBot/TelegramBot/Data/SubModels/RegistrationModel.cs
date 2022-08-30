using Newtonsoft.Json;

namespace TelegramBot.Data.SubModels
{
    public class RegistrationModel
    {
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
