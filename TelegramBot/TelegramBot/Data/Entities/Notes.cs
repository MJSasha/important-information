﻿using Newtonsoft.Json;

namespace TelegramBot.Data.Entities
{
    public class Note
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public Day Day { get; set; }
        public User User { get; set; }
    }
}
