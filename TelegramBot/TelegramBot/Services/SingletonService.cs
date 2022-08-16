using Telegram.Bot;

namespace TelegramBot.Services
{
    public static class SingletonService
    {
        private static TelegramBotClient TelegramClient { get; set; }

        public static TelegramBotClient GetClient()
        {
            if (TelegramClient == null) TelegramClient = new TelegramBotClient(AppSettings.ApiToken);
            return TelegramClient;
        }
    }
}
