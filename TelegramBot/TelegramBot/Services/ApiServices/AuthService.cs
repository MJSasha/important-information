using System;
using System.Threading.Tasks;
using TelegramBot.Data.ViewModels;
using TelegramBot.Interfaces;

namespace TelegramBot.Services.ApiServices
{
    public class AuthService : BaseService, IAuthService
    {
        public AuthService() : base(AppSettings.AuthRoot) { }

        //TODO - реализовать
        public Task Registrate(RegistrationModel registrationModel, long chatId)
        {
            throw new NotImplementedException();
        }
    }
}
