using System;
using System.Threading;
using System.Threading.Tasks;
using TelegramBot.Data.ViewModels;
using TelegramBot.Interfaces;

namespace TelegramBot.Services
{
    public class RegistrationServices
    {
        private readonly long chatId;
        private string registrationMassage;

        private Task registrationTask;
        private readonly RegistrationModel registrationModel = new();
        private CancellationTokenSource сancellationToken;
        private IBotService bot;

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

            try
            {
                сancellationToken = new CancellationTokenSource();
                registrationTask = new Task(RegName);
                bot.SendMessage("enter name:");
                registrationTask.Wait(сancellationToken.Token);
            }
            catch (SystemException)
            {
                сancellationToken.Dispose();
            }
            try
            {
                сancellationToken = new CancellationTokenSource();
                registrationTask = new Task(RegEmail);
                bot.SendMessage("enter email:");

                registrationTask = new Task(RegEmail);
                registrationTask.Wait(сancellationToken.Token);
            }
            catch (SystemException)
            {
                сancellationToken.Dispose();
            }
            try
            {
                сancellationToken = new CancellationTokenSource();
                bot.SendMessage("enter password:");

                registrationTask = new Task(RegPass);
                registrationTask.Wait(сancellationToken.Token);
            }
            catch (SystemException)
            {
                сancellationToken.Dispose();
            }

            EndRegistration();
        }

        private Func<Task> EndRegistration()
        {
            var busyUserId = DistributionService.BusyUsersIdAdnService;
            DistributionService.BusyUsersIdAdnService.RemoveAll(u => u.chatId == chatId);

            Task addToRegistration = new(() => busyUserId.RemoveAll(u => u.chatId == chatId));
            return () => addToRegistration;
        }



        private void RegName()
        {
            try
            {
                registrationModel.Name = registrationMassage;
                сancellationToken.Cancel();
            }
            catch //Тут ощибка исключения
            {

            }
        }
        private void RegEmail()
        {
            try
            {
                registrationModel.Email = registrationMassage;
                сancellationToken.Cancel();
            }
            catch { }
        }
        private void RegPass()
        {
            try
            {
                registrationModel.Password = registrationMassage;
                сancellationToken.Cancel();
            }
            catch { }
        }
    }
}
