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

        [JsonPropertyName("pictures")]
        private string pictures;

        public string[] GetPictures => pictures.Split("|");
        public void CleanPictures() => pictures = "";
        public void AddPictures(string[] pictures) => pictures.ToList().ForEach(picture => this.pictures += $"{picture}|");
        public void AddPicture(string picture) => pictures += $"{picture}|";
    }
}
