using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Data.CustomExceptions;
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

        [Obsolete]
        public static async Task SendMessage(string message, List<long> chatIds)
        {
            foreach (var chatId in chatIds)
            {
                try
                {
                    await client.SendTextMessageAsync(chatId, message);
                }
                catch (Telegram.Bot.Exceptions.ChatNotFoundException)
                {
                    throw new ChatNotFoundException(message, chatId);
                }
            }
        }

        [Obsolete]
        public async Task SendMessage(string message, IReplyMarkup buttons = null)
        {
            try
            {
                await client.SendTextMessageAsync(chatId, message, replyMarkup: buttons);
            }
            catch (Telegram.Bot.Exceptions.ChatNotFoundException)
            {
                throw new ChatNotFoundException(message, chatId);
            }
        }

        [Obsolete]
        public async Task EditMessage(string message, int messageId, IReplyMarkup buttons = null)
        {
            try
            {
                await client.EditMessageTextAsync(chatId, messageId, message, replyMarkup: (InlineKeyboardMarkup)buttons);
            }
            catch (Telegram.Bot.Exceptions.ChatNotFoundException)
            {
                throw new ChatNotFoundException(message, chatId);
            }
        }
    }
}