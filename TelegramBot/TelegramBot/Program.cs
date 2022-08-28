using System;
using System.Threading;
using TelegramBot.Messages;
using TelegramBot.Services;
using TgBotLib;
using TgBotLib.Services;

namespace TelegramBot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                BaseBotSettings.SetSettings(AppSettings.BotToken, AppSettings.ApiToken, AppSettings.BackRoot);
                var client = SingletonService.GetClient();

                client.StartReceiving();
                NewsMessages.StartMailing();
                LogService.LogStart();

                client.OnMessage += DistributionService.DistributeMessages;
                client.OnCallbackQuery += DistributionService.DistributeCallbacks;

                while (true) Thread.Sleep(int.MaxValue);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadLine();
            }
        }
    }
}