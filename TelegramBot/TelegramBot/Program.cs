using TelegramBot.Messages;
using TelegramBot.Services;
using TgBotLib;

namespace TelegramBot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BaseBotSettings.SetSettings(AppSettings.BotToken, AppSettings.ApiToken, AppSettings.BackRoot);
            BotStarter botStarter = new(DistributionService.DistributeMessages, DistributionService.DistributeCallbacks);
            botStarter.Start(NewsMessages.StartMailing);
        }
    }
}