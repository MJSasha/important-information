using ImpInfCommon.Data.Models;
using ImpInfCommon.Interfaces;
using System.Net.Http;
using System.Threading.Tasks;

namespace ImpInfCommon.ApiServices
{
    public class UsersService : BaseCRUDService<User, int>
    {
        public UsersService(string backRoot, ITokenProvider tokenProvider) : base(backRoot, tokenProvider) { }

        public async Task<User> GetByChatId(long chatId)
        {
            HttpResponseMessage httpResponse = await httpClient.GetAsync($"{Root}/ByChatId/{chatId}");
            return await Deserialize<User>(httpResponse);
        }
    }
}
