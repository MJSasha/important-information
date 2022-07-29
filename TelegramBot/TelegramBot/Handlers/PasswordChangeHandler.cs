using System;
using System.Threading.Tasks;
using TelegramBot.Data.CustomExceptions;
using TelegramBot.Services;
using TelegramBot.Services.ApiServices;
using System.Net.Http;
using TelegramBot.Data.Models;
using System.Linq;


namespace TelegramBot.Handlers
{
    public class PasswordChangeHandler : BaseSpecialHandler
    {
        private readonly User UserModel = new();
        private readonly long chatId;
        private string newPassword;

        public PasswordChangeHandler(long chatId) : base(new BotService(chatId))
        {
            this.chatId = chatId;
        }

        [Obsolete]
        public override async Task ProcessMessage(string passwordMassage)
        {
            newPassword = passwordMassage;

            if (сancellationToken == null) await Task.Run(() => ChangePassword());
            if (!сancellationToken.IsCancellationRequested) currentTask.Start();
        }
        [Obsolete]
        private void ChangePassword()
        {
            AddProcessing("Придумайте новый пароль", () => UserModel.Password.Value = newPassword, CompleteChange);
        }

        private async void CompleteChange()
        {
            try
            {
                var usersService = new UsersService();
                var currentUser = (await usersService.Get()).First(u => u.ChatId == chatId);
                if (currentUser == null) {await bot.SendMessage($"Не могу найти пользователя, возможно вы ещё не зарегестрированны");}
                else
                {
                    LogService.LogInfo($"|CHANGEPASSWORD| ChatId: {chatId} | Name: {UserModel.Name} | Login: {UserModel.Login}");
                    await usersService.Update(currentUser.Id, currentUser);
                    await bot.SendMessage($"Вы успешно сменили пароль! Ваш новый пароль {UserModel.Password.Value}");
                }
            }
            catch (HttpRequestException)
            {
                LogService.LogServerNotFound("PasswordChange");
                await bot.SendMessage("Упс, что-то пошло не так...");
            }
            finally
            {
                DistributionService.BusyUsersIdAndService.Remove(chatId);
            }
        }
    }
}
