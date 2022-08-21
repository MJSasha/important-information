using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using TelegramBot.Data.ViewModels;
using TelegramBot.Utils;
using JsonConverterAttribute = Newtonsoft.Json.JsonConverterAttribute;

namespace TelegramBot.Data.Models
{

    public class Day
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("date")]
        [JsonConverter(typeof(CustomDateTimeConverter), "yyyy-MM-dd")]
        public DateTime? Date { get; set; }

        [JsonPropertyName("information")]
        public string Information { get; set; }

        [JsonPropertyName("lessonsAndTimes")]
        public List<LessonsAndTimes> LessonsAndTimes { get; set; }

        [JsonPropertyName("currentUserNote")]
        public string CurrentUserNote { get; set; }
    }
}
