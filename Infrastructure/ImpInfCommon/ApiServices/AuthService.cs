using ImpInfCommon.Data.Models;
using ImpInfCommon.Data.Other;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TgBotLib.Exceptions;

namespace ImpInfCommon.ApiServices
{
    public class AuthService : BaseService
    {
        public AuthService(string backRoot, HttpClient httpClient, string entityRoot = null) : base(entityRoot, backRoot, httpClient) { }

        public async Task Registrate(RegistrationModel registrationModel, long chatId)
        {
            var json = Serialize(registrationModel);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var httpResponse = await httpClient.PostAsync(Root.ToString() + "/" + chatId.ToString(), data);
            if (!httpResponse.IsSuccessStatusCode) throw new ErrorResponseException(httpResponse.StatusCode, await httpResponse.Content.ReadAsStringAsync());
        }

        public async Task<User> Login(AuthModel authModel)
        {
            var json = Serialize(authModel);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var httpResponse = await httpClient.PostAsync(Root.ToString(), data);
            return await Deserialize<User>(httpResponse);
        }

        public async Task<bool> CheckToken(string token)
        {
            var httpResponse = await httpClient.GetAsync(Root.ToString() + "/CheckToken/" + token);
            if (!httpResponse.IsSuccessStatusCode) throw new ErrorResponseException(httpResponse.StatusCode, await httpResponse.Content.ReadAsStringAsync());
            return await Deserialize<bool>(httpResponse);
        }
    }
}
