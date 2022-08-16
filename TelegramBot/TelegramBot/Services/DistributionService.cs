using System;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot.Args;
using TelegramBot.Handlers;

namespace TelegramBot.Services
{
    public static class DistributionService
    {
        public static Dictionary<long, BaseSpecialHandler> BusyUsersIdAndService { get; set; } = new();

        [Obsolete]
        public static async void DistributeMessages(object sender, MessageEventArgs eventArgs)
        {
            if (!BusyUsersIdAndService.Keys.Contains(eventArgs.Message.Chat.Id)) await MainHandler.OnMessage(sender, eventArgs);

            if (BusyUsersIdAndService.Keys.Contains(eventArgs.Message.Chat.Id)) await BusyUsersIdAndService[eventArgs.Message.Chat.Id].ProcessMessage(eventArgs.Message);
        }

        [Obsolete]
        public static async void DistributeCallbacks(object sender, CallbackQueryEventArgs queryEventArgs)
        {
            BusyUsersIdAndService.Remove(queryEventArgs.CallbackQuery.Message.Chat.Id);

            if (!BusyUsersIdAndService.Keys.Contains(queryEventArgs.CallbackQuery.Message.Chat.Id)) await MainHandler.OnCallback(sender, queryEventArgs);
        }
    }
}
