using System.Threading.Tasks;
using TelegramBot.Data.ViewModels;

namespace TelegramBot.Interfaces
{
    public interface IAuthService
    {
        public Task Registrate(RegistrationModel registrationModel, long chatId);
    }
}
