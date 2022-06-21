using System.Collections.Generic;


namespace TelegramBot.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public Password Password { get; set; } 
        public string Token { get; set; }
        public long? ChatId { get; set; }
        public Role Role { get; set; }
        public List<Note> Notes { get; set; }
    }

    public enum Role
    {
        USER,
        ADMIN
    }

    public class Password
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public Password()
        {
            Id = 12;
            Value = "QwertyLang";
        }
    }  
}
