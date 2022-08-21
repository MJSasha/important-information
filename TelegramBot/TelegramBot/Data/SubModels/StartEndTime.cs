using System;
using System.Text.Json.Serialization;
using TelegramBot.Utils;

namespace TelegramBot.Data.ViewModels
{
    public class StartEndTime
    {
        [JsonPropertyName("start")]
        [Newtonsoft.Json.JsonConverter(typeof(CustomDateTimeConverter), "yyyy-MM-dd")]
        public DateTime Start { get; set; }

        [JsonPropertyName("end")]
        [Newtonsoft.Json.JsonConverter(typeof(CustomDateTimeConverter), "yyyy-MM-dd")]
        public DateTime End { get; set; }
    }
}
