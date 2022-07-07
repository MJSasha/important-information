using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using TelegramBot.Handlers;

namespace TelegramBot.Services
{
    public static class DistributionService
    {
        public static List<long> BusyUserId { get; set; }

        [Obsolete]
        public static void Collector(object sender, MessageEventArgs e)
        {
            foreach (var item in BusyUserId)
            {
                Console.WriteLine("Collector: " + item);

            }
            if (!BusyUserId.Contains(e.Message.Chat.Id)) BaseHandler.OnMessageHandler(sender, e);
            else RegistrationHandler.OnMessageHandler(sender, e);
        }

        [Obsolete]
        public static void Collector(object sender, CallbackQueryEventArgs e)
        {
            if (!BusyUserId.Contains(e.CallbackQuery.Message.Chat.Id)) BaseHandler.OnCallbackQweryHandlerAsync(sender, e);
            else RegistrationHandler.OnCallbackQweryHandlerAsync(sender, e);
        }
    }
}
