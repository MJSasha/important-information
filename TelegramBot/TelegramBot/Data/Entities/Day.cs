using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TelegramBot.Utils;

namespace TelegramBot.Data.Entities
{

    public class Day
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("date")]
        [JsonConverter(typeof(CustomDateTimeConverter), "yyyy-MM-dd")]
        public DateTime? Date { get; set; }

        [JsonProperty("information")]
        public string Information { get; set; }

        [JsonProperty("lessonsAndTimes")]
        public List<LessonsAndTimes> LessonsAndTimes { get; set; }

        [JsonProperty("currentUserNote")]
        public string CurrentUserNote { get; set; }
    }
}
