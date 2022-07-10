using System;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot.Args;
using TelegramBot.Handlers;
using TelegramBot.Interfaces;

namespace TelegramBot.Services
{
    public static class DistributionService
    {
        public static List<(long chatId, IHandler registrationServices)> BusyUsersIdAndService { get; set; } = new();

        [Obsolete]
        public static async void DistributeMessages(object sender, MessageEventArgs e)
        {
            if (!BusyUsersIdAndService.Select(u => u.chatId).Contains(e.Message.Chat.Id)) BaseHandler.OnMessage(sender, e);

            if (BusyUsersIdAndService.Select(u => u.chatId).Contains(e.Message.Chat.Id))
            {
                var selectedServiceByChatId = BusyUsersIdAndService
                    .Where(u => u.chatId == e.Message.Chat.Id).Select(u => u.registrationServices).First();
                await selectedServiceByChatId.ProcessMessage(e.Message.Text);
            }
        }

        [Obsolete]
        public static void DistributeCallbacks(object sender, CallbackQueryEventArgs e)
        {
            BusyUsersIdAndService.RemoveAll(u => u.chatId == e.CallbackQuery.Message.Chat.Id);

            if (!BusyUsersIdAndService.Select(u => u.chatId).Contains(e.CallbackQuery.Message.Chat.Id)) BaseHandler.OnCallback(sender, e);
        }
    }
}
