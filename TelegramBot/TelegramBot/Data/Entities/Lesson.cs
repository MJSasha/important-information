using Newtonsoft.Json;

namespace TelegramBot.Data.Entities
{
    public class Lesson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Teacher { get; set; }
        public string Information { get; set; }
    }
}
