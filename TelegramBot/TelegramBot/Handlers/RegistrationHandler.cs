using System;
using System.Threading;
using System.Threading.Tasks;
using TelegramBot.Data.ViewModels;
using TelegramBot.Interfaces;
using TelegramBot.Services;
using TelegramBot.Services.ApiServices;

namespace TelegramBot.Handlers
{
    public class RegistrationHandler : IHandler
    {
        private readonly RegistrationModel registrationModel = new();

        private readonly long chatId;
        private string registrationMassage;

        private readonly IBotService bot;
        private Task registrationTask;
        private CancellationTokenSource сancellationToken;

        public RegistrationHandler(long chatId)
        {
            this.chatId = chatId;
            bot = new BotService(chatId);
        }


        [Obsolete]
        public async Task ProcessMessage(string registrationMassage)
        {
            this.registrationMassage = registrationMassage;

            if (сancellationToken == null) await Task.Run(() => Registrate());
            if (!сancellationToken.IsCancellationRequested) registrationTask.Start();
        }

        [Obsolete]
        private void Registrate()
        {
            RegistrationInteration("Введите ваше имя и фамилию", () => registrationModel.Name = registrationMassage);
            RegistrationInteration("Придумайте логин", () => registrationModel.Email = registrationMassage);
            RegistrationInteration("Придумайте пароль", () => registrationModel.Password = registrationMassage, CompleteRegistration);
        }

        private void RegistrationInteration(string message, Action action, Action completeRegistrationAction = null)
        {
            try
            {
                сancellationToken = new();
                registrationTask = new Task(() =>
                {
                    action();
                    сancellationToken.Cancel();
                });
                bot.SendMessage(message);
                registrationTask.Wait();

                completeRegistrationAction?.Invoke();
            }
            catch //Обработка ошибки валидации (на будущее)
            {
                throw;
            }
        }

        private async void CompleteRegistration()
        {
            AuthService authService = new();
            await authService.Registrate(registrationModel, chatId);
            DistributionService.BusyUsersIdAndService.Remove(chatId);
            LogService.LogInfo($"ChatId: {chatId} | Name: {registrationModel.Name} | Login: {registrationModel.Email}");
            await bot.SendMessage($"Вы зарегистрированны! Теперь я буду обращаться к вам по имени {registrationModel.Name}");
        }
    }
}
