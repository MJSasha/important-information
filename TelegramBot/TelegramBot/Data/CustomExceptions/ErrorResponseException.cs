using System;
using System.Net;

namespace TelegramBot.Data.CustomExceptions
{
    public class ErrorResponseException : Exception
    {
        public HttpStatusCode statusCode;
        public string message;

        public ErrorResponseException(HttpStatusCode statusCode)
        {
            this.statusCode = statusCode;
        }

        public ErrorResponseException(HttpStatusCode statusCode, string message) : base(message)
        {
            this.statusCode = statusCode;
            this.message = message;
        }
    }
}
