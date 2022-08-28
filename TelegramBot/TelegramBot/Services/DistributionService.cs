using System;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot.Args;
using TelegramBot.Handlers;
using TgBotLib.Handlers;
using TgBotLib.Services;

namespace TelegramBot.Services
{
    public static class DistributionService
    {
        public static Dictionary<long, BaseSpecialHandler> BusyUsersIdAndService { get; set; } = new();

        public static async void DistributeMessages(object sender, MessageEventArgs eventArgs)
        {
            try
            {
                var chatId = eventArgs.Message.Chat.Id;

                if (!BusyUsersIdAndService.Keys.Contains(chatId)) await MainHandler.OnMessage(sender, eventArgs);

                if (BusyUsersIdAndService.Keys.Contains(chatId)) await BusyUsersIdAndService[chatId].ProcessMessage(eventArgs.Message);
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.ToString());
            }
        }

        public static async void DistributeCallbacks(object sender, CallbackQueryEventArgs queryEventArgs)
        {
            try
            {
                var chatId = queryEventArgs.CallbackQuery.Message.Chat.Id;
                BusyUsersIdAndService.Remove(chatId);

                if (!BusyUsersIdAndService.Keys.Contains(chatId)) await MainHandler.OnCallback(sender, queryEventArgs);

                queryEventArgs.CallbackQuery.Message.Text = null;
                if (BusyUsersIdAndService.Keys.Contains(chatId)) await BusyUsersIdAndService[chatId].ProcessMessage(queryEventArgs.CallbackQuery.Message);
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.ToString());
            }
        }
    }
}
