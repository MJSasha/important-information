using ImpInfCommon.Interfaces;
using ImpInfCommon.Utils.Attributes;
using System;
using System.Collections.Generic;

namespace ImpInfCommon.Data.Models
{
    [EntityRoot("Days")]
    public class Day : IEntity
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Information { get; set; }
        public string CurrentUserNote { get; set; }

        public List<Note> Notes { get; set; }
        public List<LessonsAndTimes> LessonsAndTimes { get; set; }
    }
}
