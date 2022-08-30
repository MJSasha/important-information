using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.Data.SubModels;
using TgBotLib.Exceptions;
using TgBotLib.Services;

namespace TelegramBot.Services.ApiServices
{
    public class AuthService : BaseService
    {
        public AuthService() : base(AppSettings.AuthRoot) { }

        public async Task Registrate(RegistrationModel registrationModel, long chatId)
        {
            var json = Serialize(registrationModel);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var httpResponse = await httpClient.PostAsync(Root.ToString() + "/" + chatId.ToString(), data);
            if (!httpResponse.IsSuccessStatusCode) throw new ErrorResponseException(httpResponse.StatusCode,
                                                                await httpResponse.Content.ReadAsStringAsync());
        }
    }
}
