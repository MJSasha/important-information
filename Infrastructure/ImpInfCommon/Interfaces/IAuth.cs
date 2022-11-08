using ImpInfCommon.Data.Models;
using ImpInfCommon.Data.Other;
using System.Threading.Tasks;

namespace ImpInfCommon.Interfaces
{
    public interface IAuth
    {
        Task Registrate(RegistrationModel registrationModel, long chatId);
        Task<User> Login(AuthModel authModel);
        Task<bool> CheckToken(string token);
    }
}
