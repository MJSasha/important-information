using TelegramBot.Data.Models;

namespace TelegramBot.Services.ApiServices
{
    public class UsersServices : BaseCRUDService<User, int>
    {
        public UsersServices() : base(AppSettings.UsersRoot)
        {
        }
    }
}
