﻿using ImpInfCommon.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Utils;
using TgBotLib.Exceptions;
using TgBotLib.Interfaces;
using TgBotLib.Services;

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

        public static async Task SendNews(News news, List<long> chatIds, IReplyMarkup buttons = null)
        {
            foreach (var chatId in chatIds)
            {
                try
                {
                    try
                    {
                        if (!string.IsNullOrWhiteSpace(news.Pictures))
                        {
                            string[] media;
                            media = news.GetPictures();
                            List<InputMediaPhoto> albumInputMedias = news.GetPictures().Select(p => new InputMediaPhoto(p)).ToList();
                            await client.SendMediaGroupAsync(chatId, albumInputMedias);
                        }
                    }
                    catch { /*ignore invalid pictures*/}

                    if (!string.IsNullOrWhiteSpace(news.Message)) await client.SendTextMessageAsync(chatId, news.GetNewsCard(), replyMarkup: buttons, parseMode: ParseMode.Markdown);
                }
                catch (Telegram.Bot.Exceptions.ChatNotFoundException)
                {
                    throw new ChatNotFoundException(news?.Message, chatId);
                }
            }
        }

        public async Task DeleteMessage(int messageId)
        {
            try
            {
                await client.DeleteMessageAsync(chatId, messageId);
            }
            catch { /*ignore*/}
        }

        public async Task SendMessage(string message, IReplyMarkup buttons = null)
        {
            try
            {
                await client.SendTextMessageAsync(chatId, message, replyMarkup: buttons, parseMode: ParseMode.Markdown);
            }
            catch (Telegram.Bot.Exceptions.ChatNotFoundException)
            {
                throw new ChatNotFoundException(message, chatId);
            }
        }

        public async Task EditMessage(string message, int messageId, IReplyMarkup buttons = null)
        {
            try
            {
                await client.EditMessageTextAsync(chatId, messageId, message, replyMarkup: (InlineKeyboardMarkup)buttons, parseMode: ParseMode.Markdown);
            }
            catch (Telegram.Bot.Exceptions.ChatNotFoundException)
            {
                throw new ChatNotFoundException(message, chatId);
            }
            catch (Telegram.Bot.Exceptions.MessageIsNotModifiedException) { /*ignore*/ }
        }
    }
}