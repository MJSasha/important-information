using ImpInfCommon.Data.Models;
using System.Net.Http;
using System.Threading.Tasks;
using TgBotLib.Services;

namespace TelegramBot.Services.ApiServices
{
    public class UsersService : BaseCRUDService<User, int>
    {
        public UsersService() { }

        public async Task<User> GetByChatId(long chatId)
        {
            HttpResponseMessage httpResponse = await httpClient.GetAsync($"{Root}/ByChatId/{chatId}");
            return await Deserialize<User>(httpResponse);
        }
    }
}
