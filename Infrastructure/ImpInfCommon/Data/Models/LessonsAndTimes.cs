using ImpInfCommon.Data.Definitions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImpInfCommon.Data.Models
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
