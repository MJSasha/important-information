﻿using System;
using System.Threading;
using System.Threading.Tasks;
using TelegramBot.Data.ViewModels;
using TelegramBot.Interfaces;
using TelegramBot.Services.ApiServices;

namespace TelegramBot.Services
{
    public class RegistrationServices
    {
        private readonly RegistrationModel registrationModel = new();

        private readonly long chatId;
        private string registrationMassage;

        private IBotService bot;
        private Task registrationTask;
        private CancellationTokenSource сancellationToken;

        public RegistrationServices(long chatId)
        {
            this.chatId = chatId;
        }


        [Obsolete]
        public async Task ContinueRegistration(string registrationMassage)
        {
            this.registrationMassage = registrationMassage;

            if (сancellationToken == null) await Task.Run(() => Registrate());
            if (!сancellationToken.IsCancellationRequested) registrationTask.Start();
        }

        [Obsolete]
        private void Registrate()
        {
            bot = new BotService(chatId);

            RegistrationInteration("Введите ваше имя", () => registrationModel.Name = registrationMassage);
            RegistrationInteration("Придумайте логин", () => registrationModel.Email = registrationMassage);
            RegistrationInteration("Придумайте пароль", () => registrationModel.Password = registrationMassage);

            EndRegistration();
        }

        private void RegistrationInteration(string message, Action action)
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
            }
            catch //Обработка ошибки валидации (на будущее)
            {
                throw;
            }
        }

        private Func<Task> EndRegistration()
        {
            return async () =>
            {
                AuthService authService = new();
                await authService.Registrate(registrationModel, chatId);

                await bot.SendMessage("Регистрация закончена");

                DistributionService.BusyUsersIdAdnService.RemoveAll(u => u.chatId == chatId);
            };
        }
    }
}