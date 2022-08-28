using Newtonsoft.Json;
using System;
using TelegramBot.Utils;

namespace TelegramBot.Data.SubModels
{
    public class StartEndTime
    {
        [JsonProperty("start")]
        [JsonConverter(typeof(CustomDateTimeConverter), "yyyy-MM-dd")]
        public DateTime Start { get; set; }

        [JsonProperty("end")]
        [JsonConverter(typeof(CustomDateTimeConverter), "yyyy-MM-dd")]
        public DateTime End { get; set; }
    }
}
