using System;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using TelegramBot.Messages;
using TelegramBot.Services;

namespace TelegramBot
{
    internal class Program
    {
        [Obsolete]
        static async Task Main(string[] args)
        {
            try
            {
                var client = SingletonService.GetClient();
                await NewsMessages.StartMailing();

                client.StartReceiving();

                LogService.LogStart();

                client.OnMessage += DistributionService.Distributor;
                client.OnMessage += LogService.LogMessages;
                client.OnCallbackQuery += DistributionService.Distributor;
                client.OnCallbackQuery += LogService.LogCallbacks;

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
