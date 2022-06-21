using TelegramBot.Models;

namespace TelegramBot.Repositories
{
    public class PostUserRepository : BaseRepository<PostUser, int>
    {
        public PostUserRepository() : base(AppSettings.UsersRoot)
        {
        }
    }
}
