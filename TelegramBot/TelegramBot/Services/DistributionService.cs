using System;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot.Args;
using TelegramBot.Handlers;

namespace TelegramBot.Services
{
    public static class DistributionService
    {
        public static List<(long chatId, RegistrationServices registrationServices)> BusyUsersIdAdnService { get; set; } = new();

        [Obsolete]
        public static void Collector(object sender, MessageEventArgs e)
        {
            if (!BusyUsersIdAdnService.Select(u => u.chatId).Contains(e.Message.Chat.Id)) BaseHandler.OnMessageHandler(sender, e);
            if (BusyUsersIdAdnService.Select(u => u.chatId).Contains(e.Message.Chat.Id)) RegistrationHandler.OnMessage(sender, e);
        }

        [Obsolete]
        public static void Collector(object sender, CallbackQueryEventArgs e)
        {
            if (!BusyUsersIdAdnService.Select(u => u.chatId).Contains(e.CallbackQuery.Message.Chat.Id)) BaseHandler.OnCallbackQweryHandlerAsync(sender, e);
            else RegistrationHandler.OnCallback(sender, e);
        }
    }
}
