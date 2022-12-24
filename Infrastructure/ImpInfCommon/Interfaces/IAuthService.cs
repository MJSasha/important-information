using ImpInfCommon.Data.Models;
using ImpInfCommon.Data.Other;
using Refit;
using System.Threading.Tasks;

namespace ImpInfCommon.Interfaces
{
    public interface IAuthService
    {
        [Post("/Account/{chatId}")]
        Task Registrate([Body] RegistrationModel registrationModel, long chatId);
        [Post("/Account")]
        Task<User> Login([Body] AuthModel authModel);
        [Get("/Account/CheckToken/{token}")]
        Task<bool> CheckToken(string token);
        [Get("/Account/CurrentUser")]
        Task<User> GetCurrentUser();
    }
}
