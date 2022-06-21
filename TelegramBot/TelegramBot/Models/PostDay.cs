using System;
using System.Collections.Generic;

namespace TelegramBot.Models
{

    public class PostDay
    {
        public DateTime Date { get; set; } // "dd-MM-yyyy"
        public string Information { get; set; }
        public List<PostLessonsAndtimes> LessonsAndTimes { get; set; }

        public PostDay()
        {
            Date = new DateTime(2022, 06, 21);
            Information = "Basic information in PostDay";
            LessonsAndTimes = new List<PostLessonsAndtimes> { new PostLessonsAndtimes() };

        }
    }

    public class PostLessonsAndtimes
    {
        public DateTime Time { get; set; }   //"HH:mm:ss"
        public PostLesson Lesson { get; set; }

        public PostLessonsAndtimes()
        {
            Time = DateTime.Now;
            Lesson = new PostLesson();
        }

    }

    public class PostLesson
    {
        public string Name { get; set; }
        public string Teacher { get; set; }

        public PostLesson()
        {
            Name = "nameLanguage";
            Teacher = "Stanok";
        }
    }


}
