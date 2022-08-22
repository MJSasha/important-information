using Newtonsoft.Json;
using System;
using TelegramBot.Utils;

namespace TelegramBot.Data.ViewModels
{
    public class StartEndTime
    {
        [JsonProperty("start")]
        [Newtonsoft.Json.JsonConverter(typeof(CustomDateTimeConverter), "yyyy-MM-dd")]
        public DateTime Start { get; set; }

        [JsonProperty("end")]
        [Newtonsoft.Json.JsonConverter(typeof(CustomDateTimeConverter), "yyyy-MM-dd")]
        public DateTime End { get; set; }
    }
}
