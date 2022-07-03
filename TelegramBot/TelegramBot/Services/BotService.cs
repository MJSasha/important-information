using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Interfaces;

namespace TelegramBot.Services
{
    public class BotService : IBotService
    {
        private readonly long chatId;
        private readonly TelegramBotClient client = new(AppSettings.Token);

        public BotService(long chatId)
        {
            this.chatId = chatId;
        }

        public async Task SendMessage(string message)
        {
            await client.SendTextMessageAsync(chatId, message);
        }
        public async Task SendMessage(string message, long chatId)
        {
            await client.SendTextMessageAsync(chatId, message);
        }
        public async Task SendMessage(string message, IReplyMarkup buttons)
        {
            await client.SendTextMessageAsync(chatId, message, replyMarkup: buttons);
        }

        public async Task EditMessage(string message, int messageId)
        {
            await client.EditMessageTextAsync(chatId, messageId, message);
        }
        public async Task EditMessage(string message, IReplyMarkup buttons, int messageId)
        {
            await client.EditMessageTextAsync(chatId, messageId, message, replyMarkup: (InlineKeyboardMarkup)buttons);
        }
    }
}
