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
        public static async void DistributeMessages(object sender, MessageEventArgs e)
        {
            if (!BusyUsersIdAndService.Keys.Contains(e.Message.Chat.Id)) await BaseHandler.OnMessage(sender, e);

            if (BusyUsersIdAndService.Keys.Contains(e.Message.Chat.Id)) await BusyUsersIdAndService[e.Message.Chat.Id].ProcessMessage(e.Message.Text);
        }

        [Obsolete]
        public static async void DistributeCallbacks(object sender, CallbackQueryEventArgs e)
        {
            if (!BusyUsersIdAndService.Keys.Contains(e.CallbackQuery.Message.Chat.Id)) await BaseHandler.OnCallback(sender, e);

            if (BusyUsersIdAndService.Keys.Contains(e.CallbackQuery.Message.Chat.Id)) await BusyUsersIdAndService[e.CallbackQuery.Message.Chat.Id].ProcessMessage(e.CallbackQuery.Message.Text);
        }
    }
}
