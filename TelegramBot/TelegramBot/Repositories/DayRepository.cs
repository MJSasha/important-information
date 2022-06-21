
using TelegramBot.Models;

namespace TelegramBot.Repositories
{
    public class DayRepository : BaseRepository<Day, int>
    {
        public DayRepository() : base(AppSettings.DaysRoot)
        {
        }
    }
}
