using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TelegramBot.Data.CustomExceptions;
using TelegramBot.Data.ViewModels;
using TelegramBot.Interfaces;

namespace TelegramBot.Services.ApiServices
{
    public class AuthService : BaseService, IAuthService
    {
        public AuthService() : base(AppSettings.AuthRoot) { }

        public async Task Registrate(RegistrationModel registrationModel, long chatId)
        {
            var json = JsonSerializer.Serialize(registrationModel);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var httpResponse = await httpClient.PostAsync(Root.ToString() + "/" + chatId.ToString(), data);
            if (!httpResponse.IsSuccessStatusCode) throw new ErrorResponseException(httpResponse.StatusCode,
                                                                await httpResponse.Content.ReadAsStringAsync());
        }
    }
}
