using Telegram.Bot;

namespace TelegramBot.Services
{
    public static class SingletonService
    {
        public static TelegramBotClient TelegramClient { get; set; }
    }
}
