using TelegramBot.Data.ViewModels;

namespace TelegramBot.Services.ApiServices
{
    public class LessonsService : BaseCRUDService<Lesson, int>
    {
        public LessonsService() : base(AppSettings.LessonsRoot)
        {
        }
    }
}
