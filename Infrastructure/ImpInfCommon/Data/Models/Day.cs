using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ImpInfCommon.Data.Models
{
    public class Day
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Information { get; set; }
        public List<LessonsAndTimes> LessonsAndTimes { get; set; }
        public string CurrentUserNote { get; set; }
    }
}
