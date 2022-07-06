using System;
using Telegram.Bot;
using TelegramBot.Services;
using TelegramBot.Handlers;
using Telegram.Bot.Args;
using TelegramBot.Messages;
using System.Collections.Generic;

namespace TelegramBot
{
    internal class Program
    {
        [Obsolete]
        static void Main(string[] args)
        {
            try
            {
                var client = new TelegramBotClient(AppSettings.Token);
                SingletonService.TelegramClient = client;

                var busyUsers = new List<long>() { 999999999 };
                BusyUsers.BusyUserId = busyUsers;


                client.StartReceiving();
                client.OnMessage += Collector;
                client.OnMessage += LogService.MessageLogging;
                client.OnCallbackQuery += Collector;
                client.OnCallbackQuery += LogService.CallbackLogging;
                Console.ReadLine();
                client.StopReceiving();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadLine();
            }
        }

        [Obsolete]
        public static void Collector(object sender, MessageEventArgs e)
        {
            if (!BusyUsers.BusyUserId.Contains(e.Message.Chat.Id)) BaseHandler.OnMessageHandler(sender, e);
            else RegistrationHandler.OnMessageHandler(sender, e);
        }

        [Obsolete]
        public static void Collector(object sender, CallbackQueryEventArgs e)
        {
            if (!BusyUsers.BusyUserId.Contains(e.CallbackQuery.Message.Chat.Id)) BaseHandler.OnCallbackQweryHandlerAsync(sender, e);
            else RegistrationHandler.OnCallbackQweryHandlerAsync(sender, e);
        }
    }
}
