using System.Linq;
using System.Text.Json.Serialization;

namespace TelegramBot.Data.Models
{
    public class News
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("needToSend")]
        public bool NeedToSend { get; set; }

        /// <summary>
        /// Get публичный для сериализации, для получения массива картинок используйте метод <see cref="GetPictures"/>.
        /// </summary>
        [JsonPropertyName("pictures")]
        public string Pictures { get; private set; }

        public string[] GetPictures() => Pictures.Split("|");
        public void CleanPictures() => Pictures = "";
        public void AddPictures(string[] pictures) => pictures.ToList().ForEach(picture => this.Pictures += $"{picture}|");
        public void AddPicture(string picture) => Pictures += $"{picture}|";
    }
}
