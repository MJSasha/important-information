using ImpInfCommon.Interfaces;
using System;
using System.Linq;
using TgBotLib.Utils.Attributes;

namespace ImpInfCommon.Data.Models
{
    [EntityRoot("News")]
    public class News : IEntity
    {
        public int Id { get; set; }
        public DateTime DateTimeOfCreate { get; set; } = DateTime.Now;
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
