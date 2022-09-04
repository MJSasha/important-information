using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TelegramBot.Utils;

namespace TelegramBot.Data.Entities
{

    public class Day
    {
        public int Id { get; set; }
        [JsonConverter(typeof(CustomDateTimeConverter), "yyyy-MM-dd")]
        public DateTime? Date { get; set; }
        public string Information { get; set; }
        public List<LessonsAndTimes> LessonsAndTimes { get; set; }
        public string CurrentUserNote { get; set; }
    }
}
