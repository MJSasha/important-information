using Newtonsoft.Json;
using System;
using System.Text.Json.Serialization;
using TelegramBot.Data.ViewModels;
using TelegramBot.Utils;

namespace TelegramBot.Data.Models
{
    public class News
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("dateTimeOfCreate")]
        [JsonConverter(typeof(CustomDateTimeConverter), "yyyy-MM-dd HH:mm:ss")]
        public DateTime DateTimeOfCreate { get; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("needToSend")]
        public bool NeedToSend { get; set; } = false;

        [JsonProperty("lesson")]
        public Lesson Lesson { get; set; }

        /// <summary>
        /// Get публичный для сериализации, для получения массива картинок используйте метод <see cref="GetPictures"/>.
        /// </summary>
        [JsonProperty("pictures")]
        public string Pictures { get; set; }

        public string[] GetPictures() => Pictures?.Split("|", StringSplitOptions.RemoveEmptyEntries);
        public void CleanPictures() => Pictures = "";
        public void AddPicture(string picture) => Pictures += $"{picture}|";
    }
}
