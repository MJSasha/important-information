using System;
using System.Threading.Tasks;
using TelegramBot.Data.CustomExceptions;
using TelegramBot.Data.ViewModels;
using TelegramBot.Services;
using TelegramBot.Services.ApiServices;
using System.Net.Http;


namespace TelegramBot.Handlers
{
    public class PasswordHandler : BaseSpecialHandler
    {
        private readonly RegistrationModel registrationModel = new();
        private readonly long chatId;
        private string registrationMassage;

        public PasswordHandler(long chatId) : base(new BotService(chatId))
        {
            this.chatId = chatId;
        }
        
        [Obsolete]
        public override async Task ProcessMessage(string registrationMassage)
        {
            this.registrationMassage = registrationMassage;

            if (сancellationToken == null) await Task.Run(() => Change());
            if (!сancellationToken.IsCancellationRequested) currentTask.Start();
        }
        [Obsolete]
        private void Change()
        {
            AddProcessing("Придумайте новый пароль", () => registrationModel.Password = registrationMassage, CompleteChange);
        }

        private async void CompleteChange()
        {
            try
            {
                AuthService authService = new();
                await authService.Registrate(registrationModel, chatId);
                LogService.LogInfo($"|CHANGEPASSWORD| ChatId: {chatId} | Name: {registrationModel.Name} | Login: {registrationModel.Login}");
                await bot.SendMessage($"Вы успешно сменили пароль! Ваш новый пароль {registrationModel.Password}");
            }
            catch (ErrorResponseException)
            {
                await bot.SendMessage("Мде... походу такой логин уже существует, дружище. Давай по новой (/reg)");
            }
            catch (HttpRequestException)
            {
                LogService.LogServerNotFound("Registration");
                await bot.SendMessage("Упс, что-то пошло не так...");
            }
            finally
            {
                DistributionService.BusyUsersIdAndService.Remove(chatId);
            }
        }
    }
}
