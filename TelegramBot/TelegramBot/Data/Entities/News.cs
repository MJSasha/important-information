using Newtonsoft.Json;
using System;
using System.Linq;
using TelegramBot.Utils;

namespace TelegramBot.Data.Entities
{
    public class News
    {
        public int Id { get; set; }
        [JsonConverter(typeof(CustomDateTimeConverter), "yyyy-MM-dd HH:mm:ss")]
        public DateTime DateTimeOfCreate { get; set; }
        public string Message { get; set; }
        public bool NeedToSend { get; set; } = false;
        public Lesson Lesson { get; set; }
        /// <summary>
        /// Get публичный для сериализации, для получения массива картинок используйте метод <see cref="GetPictures"/>.
        /// </summary>
        public string Pictures { get; set; }

        public string[] GetPictures() => Pictures?.Split("|", StringSplitOptions.RemoveEmptyEntries);
        public void CleanPictures() => Pictures = "";
        public void AddPictures(string[] pictures) => pictures.ToList().ForEach(picture => Pictures += $"{picture}|");
        public void AddPicture(string picture) => Pictures += $"{picture}|";
    }
}
