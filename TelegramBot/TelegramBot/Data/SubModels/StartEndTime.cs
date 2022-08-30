using Newtonsoft.Json;
using System;
using TelegramBot.Utils;

namespace TelegramBot.Data.SubModels
{
    public class StartEndTime
    {
        [JsonConverter(typeof(CustomDateTimeConverter), "yyyy-MM-dd")]
        public DateTime Start { get; set; }
        [JsonConverter(typeof(CustomDateTimeConverter), "yyyy-MM-dd")]
        public DateTime End { get; set; }
    }
}
