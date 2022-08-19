﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramBot.Data;
using TelegramBot.Services;
using TelegramBot.Services.ApiServices;

namespace TelegramBot.Handlers
{
    public class PasswordChangeHandler : BaseSpecialHandler
    {
        private readonly long chatId;
        private string newPassword;

        public PasswordChangeHandler(long chatId) : base(new BotService(chatId))
        {
            this.chatId = chatId;
        }

        [Obsolete]
        public override async Task ProcessMessage(Message newPassword)
        {
            this.newPassword = newPassword.Text;
            await base.ProcessMessage(newPassword);
        }

        [Obsolete]
        protected override void RegistrateProcessing()
        {
            AddProcessing("Придумайте новый пароль", int.MaxValue, CompleteChange);
        }

        private async void CompleteChange()
        {
            try
            {
                var usersService = new UsersService();
                var currentUser = await usersService.GetByChatId(chatId);
                if (currentUser == null) { await bot.SendMessage($"Не могу найти пользователя, возможно вы ещё не зарегестрированны (/reg)"); }
                else
                {
                    currentUser.Password.Value = newPassword;
                    await usersService.Update(currentUser.Id, currentUser);
                    LogService.LogInfo($"|CHANGEPASSWORD| ChatId: {chatId} | Name: {currentUser.Name} | Login: {currentUser.Login}");
                    await bot.SendMessage($"Вы успешно сменили пароль! Ваш новый пароль {currentUser.Password.Value}");
                }
            }
            catch (HttpRequestException)
            {
                LogService.LogServerNotFound("PasswordChange");
                await bot.SendMessage(Texts.Oops);
            }
            finally
            {
                DistributionService.BusyUsersIdAndService.Remove(chatId);
            }
        }
    }
}
