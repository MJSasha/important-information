using System.Net.Http;
using System.Threading.Tasks;
using TelegramBot.Data.Entities;
using TgBotLib.Services;

namespace TelegramBot.Services.ApiServices
{
    public class UsersService : BaseCRUDService<User, int>
    {
        public UsersService() : base(AppSettings.UsersRoot)
        {
        }

        public async Task<User> GetByChatId(long chatId)
        {
            HttpResponseMessage httpResponse = await httpClient.GetAsync($"{Root}/byChatId/{chatId}");
            return await Deserialize<User>(httpResponse);
        }
    }
}
