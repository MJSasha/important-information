using System;
using System.Collections.Generic;

namespace TelegramBot.Models
{

    public class Day
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }  // "dd-MM-yyyy"
        public string Information { get; set; }
        public List<LessonsAndTimes>? LessonsAndTimes { get; set; }
        //public Note CurrentUserNote { get; set; }
    }

    public class LessonsAndTimes
    {
        public int Id { get; set; }
        public DateTime? Time { get; set; }    //"HH:mm:ss"
        public Lesson Lesson { get; set; }
    }

    public class Lesson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Teacher { get; set; }
        //public string Information { get; set; }
    }
}
