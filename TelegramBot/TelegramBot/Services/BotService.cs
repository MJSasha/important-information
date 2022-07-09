using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Interfaces;

namespace TelegramBot.Services
{
    public class BotService : IBotService
    {
        private readonly long chatId;
        private static readonly TelegramBotClient client = SingletonService.GetClient();

        public BotService(long chatId)
        {
            this.chatId = chatId;
        }

        //TODO - id не может быть null, это нужно будет поправить и в бэке (в тестовой бд), и тут
        public static async Task SendMessage(string message, List<long?> chatIds)
        {
            foreach (var id in chatIds)
            {
                if (id == null) continue;
                await client.SendTextMessageAsync(id, message);
            }
        }

        public async Task SendMessage(string message)
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