using TelegramBot.Models;

namespace TelegramBot.Repositories
{
    public class CurrentUserRepository : BaseRepository<User, int>
    {
        public CurrentUserRepository() : base(AppSettings.CurrentUsers)
        {
        }
    }
}
