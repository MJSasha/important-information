using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using TelegramBot.Data.ViewModels;
using TelegramBot.Utils;

namespace TelegramBot.Data.Models
{

    public class Day
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("date")]
        [Newtonsoft.Json.JsonConverter(typeof(CustomDateTimeConverter), "yyyy-MM-dd")]
        public DateTime? Date { get; set; }

        [JsonPropertyName("information")]
        public string Information { get; set; }

        [JsonPropertyName("lessonsAndTimes")]
        public List<LessonsAndTimes> LessonsAndTimes { get; set; }

        [JsonPropertyName("currentUserNote")]
        public string CurrentUserNote { get; set; }
    }
}
