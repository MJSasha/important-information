using TelegramBot.Data.Models;

namespace TelegramBot.Services.ApiServices
{
    public class UsersService : BaseCRUDService<User, int>
    {
        public UsersService() : base(AppSettings.UsersRoot)
        {
        }
    }
}
