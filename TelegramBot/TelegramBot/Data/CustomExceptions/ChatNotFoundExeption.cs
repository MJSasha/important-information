using System;

namespace TelegramBot.Data.CustomExceptions
{
    public class ChatNotFoundException : Exception
    {
        public override string Message { get; }
        public long ChatId { get; }

        public ChatNotFoundException(string message, long chatId) : base(message)
        {
            Message = message;
            ChatId = chatId;
        }
    }
}
