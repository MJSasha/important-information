using Newtonsoft.Json;
using System.Collections.Generic;
using TelegramBot.Data.Definitions;

namespace TelegramBot.Data.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public Password Password { get; set; }
        public string Token { get; set; }
        public long ChatId { get; set; }
        public Role Role { get; set; }
        public List<Note> Notes { get; set; }
    }
}
