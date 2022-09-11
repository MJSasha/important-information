using System;
using System.Collections.Generic;

namespace ImpInfApi.Data.Entities
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
