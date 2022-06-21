using TelegramBot.Models;

namespace TelegramBot.Repositories
{
    public class PostDayRepository : BaseRepository<PostDay, int>
    {
        public PostDayRepository() : base(AppSettings.DaysRoot)
        {
        }
    }
}
