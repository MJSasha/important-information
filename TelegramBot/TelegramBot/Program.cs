using System;
using Telegram.Bot;
using TelegramBot.Services;

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

                client.StartReceiving();
                client.OnMessage += DistributionService.Collector;
                client.OnMessage += LogService.MessageLogging;
                client.OnCallbackQuery += DistributionService.Collector;
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
    }
}
