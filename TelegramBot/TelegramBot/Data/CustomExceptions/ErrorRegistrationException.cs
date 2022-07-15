using System;
using System.Net;

namespace TelegramBot.Data.CustomExceptions
{
    public class ErrorRegistrationException : Exception
    {
        public string message;

        public ErrorRegistrationException(string message) : base(message)
        {
            this.message = message;
        }
    }
}
