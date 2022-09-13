using System;
using System.ComponentModel.DataAnnotations;

namespace ImpInfCommon.Data.Other
{
    public class DateTimeWrap
    {
        [Required]
        public DateTime DateTime { get; set; }
    }
}
