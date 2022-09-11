using ImpInfApi.Data.Definitions;
using System;
using System.Collections.Generic;

namespace ImpInfApi.Data.Entities
{
    public class LessonsAndTimes
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public LessonType Type { get; set; }
        public Lesson Lesson { get; set; }

        public List<Day> Days { get; set; }
    }
}
