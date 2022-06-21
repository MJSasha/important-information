using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using TelegramBot.Data.ViewModels;

namespace TelegramBot.Data.Models
{

    public class Day
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("date")]
        public DateTime? Date { get; set; }  // "dd-MM-yyyy"
        [JsonPropertyName("information")]
        public string Information { get; set; }
        [JsonPropertyName("lessonsAndTimes")]
        public List<LessonsAndTimes> LessonsAndTimes { get; set; }
        [JsonPropertyName("currentUserNote")]
        public string CurrentUserNote { get; set; }
    }
}
