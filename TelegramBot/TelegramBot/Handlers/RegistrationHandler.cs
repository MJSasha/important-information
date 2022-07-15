using System;
using System.Threading.Tasks;
using TelegramBot.Data.ViewModels;
using TelegramBot.Services;
using TelegramBot.Services.ApiServices;

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
        public override async Task ProcessMessage(string registrationMassage)
        {
            this.registrationMassage = registrationMassage;

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
            //Вот тут надо провалидировать модель
            AuthService authService = new();
            await authService.Registrate(registrationModel, chatId);
            DistributionService.BusyUsersIdAndService.Remove(chatId);
            LogService.LogInfo($"|REGISTRATION| ChatId: {chatId} | Name: {registrationModel.Name} | Login: {registrationModel.Login}");
            await bot.SendMessage($"Вы зарегистрированны! Теперь я буду обращаться к вам по имени {registrationModel.Name}");
        }
    }
}
