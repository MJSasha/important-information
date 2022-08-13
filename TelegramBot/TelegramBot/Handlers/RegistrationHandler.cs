using System;
using System.Net.Http;
using System.Threading.Tasks;
using TelegramBot.Data.CustomExceptions;
using TelegramBot.Data.ViewModels;
using TelegramBot.Services;
using TelegramBot.Services.ApiServices;
using Telegram.Bot.Args;

namespace TelegramBot.Handlers
{
    public class RegistrationHandler : BaseSpecialHandler
    {
        private readonly RegistrationModel registrationModel = new();
        private readonly long chatId;
        private string registrationMassage;

        public RegistrationHandler(long chatId) : base(new BotService(chatId))
        {
            this.chatId = chatId;
        }

        [Obsolete]
        public override async Task ProcessMessage(Telegram.Bot.Types.Message registrationMassage)
        {
            this.registrationMassage = registrationMassage.Text;

            if (сancellationToken == null) await Task.Run(() => Registrate());
            if (!сancellationToken.IsCancellationRequested) currentTask.Start();
        }

        [Obsolete]
        private void Registrate()
        {
            AddProcessing("Введите ваше имя и фамилию", () => registrationModel.Name = registrationMassage);
            AddProcessing("Придумайте логин", () => registrationModel.Login = registrationMassage);
            AddProcessing("Придумайте пароль", () => registrationModel.Password = registrationMassage, CompleteRegistration);
        }

        private async void CompleteRegistration()
        {
            try
            {
                AuthService authService = new();
                await authService.Registrate(registrationModel, chatId);
                LogService.LogInfo($"|REGISTRATION| ChatId: {chatId} | Name: {registrationModel.Name} | Login: {registrationModel.Login}");
                await bot.SendMessage($"Вы зарегистрированны! Теперь я буду обращаться к вам по имени {registrationModel.Name}");
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
