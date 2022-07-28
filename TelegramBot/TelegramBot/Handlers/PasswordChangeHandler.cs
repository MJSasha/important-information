using System;
using System.Threading.Tasks;
using TelegramBot.Data.CustomExceptions;
using TelegramBot.Data.ViewModels;
using TelegramBot.Services;
using TelegramBot.Services.ApiServices;
using System.Net.Http;
using TelegramBot.Data.Models;

namespace TelegramBot.Handlers
{
    public class PasswordChangeHandler : BaseSpecialHandler
    {
        private readonly User UserModel = new();
        private readonly long chatId;
        private string passwordMassage;

        public PasswordChangeHandler(long chatId) : base(new BotService(chatId))
        {
            this.chatId = chatId;
        }

        [Obsolete]
        public override async Task ProcessMessage(string passwordMassage)
        {
            this.passwordMassage = passwordMassage;

            if (сancellationToken == null) await Task.Run(() => ChangePassword());
            if (!сancellationToken.IsCancellationRequested) currentTask.Start();
        }
        [Obsolete]
        private async void ChangePassword()
        {
            AddProcessing("Придумайте новый пароль", () => UserModel.Password.Value = passwordMassage, CompleteChange);
            UserModel.Password.Value = passwordMassage;
            //
            var usersService = new UsersService();
            var users = await usersService.Get();
            foreach (var user in users)
            {
                if (user.Id == chatId)
                {
                    user.Password.Value = passwordMassage;
                    await usersService.Update(user.Id, user);
                }
            }
            //
        }

        private async void CompleteChange()
        {
            try
            {
                LogService.LogInfo($"|CHANGEPASSWORD| ChatId: {chatId} | Name: {UserModel.Name} | Login: {UserModel.Login}");
                await bot.SendMessage($"Вы успешно сменили пароль! Ваш новый пароль {UserModel.Password.Value}");
            }
            catch (ErrorResponseException)
            {
                await bot.SendMessage("Мде... походу такой логин уже существует, дружище. Давай по новой (/pass)");
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
