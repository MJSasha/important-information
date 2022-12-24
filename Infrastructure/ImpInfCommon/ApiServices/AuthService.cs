using ImpInfCommon.Data.Models;
using ImpInfCommon.Data.Other;
using ImpInfCommon.Interfaces;
using ImpInfCommon.Utils;
using System.Net.Http;
using System.Threading.Tasks;

namespace ImpInfCommon.ApiServices
{
    public class AuthService : IAuthService
    {
        private readonly IAuthService authService;
        private readonly IErrorsHandler errorsHandler;

        public AuthService(HttpClient httpClient, IErrorsHandler errorsHandler)
        {
            authService = UtilsFunctions.GetRefitService<IAuthService>(httpClient);
            this.errorsHandler = errorsHandler;
        }

        public Task Registrate(RegistrationModel registrationModel, long chatId)
        {
            return errorsHandler.SaveExecute(async () => await authService.Registrate(registrationModel, chatId));
        }

        public async Task<User> Login(AuthModel authModel)
        {
            User result = default;
            await errorsHandler.SaveExecute(async () => result = await authService.Login(authModel));
            return result;
        }

        public async Task<bool> CheckToken(string token)
        {
            bool result = default;
            await errorsHandler.SaveExecute(async () => result = await authService.CheckToken(token));
            return result;
        }

        public async Task<User> GetCurrentUser()
        {
            User result = default;
            await errorsHandler.SaveExecute(async () => result = await authService.GetCurrentUser());
            return result;
        }
    }
}
