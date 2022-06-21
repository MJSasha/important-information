using TelegramBot.Models;

namespace TelegramBot.Repositories
{
    public class UserRepository : BaseRepository<User, int>
    {
        public UserRepository() : base(AppSettings.UsersRoot)
        {
        }
    }
}
