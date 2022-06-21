using System;


namespace TelegramBot.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Discription { get; set; }
        public DateTime Day { get; set; }
        public User User { get; set; }
    }
}
