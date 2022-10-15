using ImpInfCommon.Data.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace ImpInfCommon.ApiServices
{
    public class UsersService : BaseCRUDService<User, int>
    {
        public UsersService(string backRoot, string token = "") : base(backRoot, token: token) { }

        public async Task<User> GetByChatId(long chatId)
        {
            HttpResponseMessage httpResponse = await httpClient.GetAsync($"{Root}/ByChatId/{chatId}");
            return await Deserialize<User>(httpResponse);
        }
    }
}
