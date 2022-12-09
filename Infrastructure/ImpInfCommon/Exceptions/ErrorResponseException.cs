using System;
using System.Net;

namespace ImpInfCommon.Exceptions
{
    public class ErrorResponseException : Exception
    {
        public HttpStatusCode StatusCode { get; }
        public override string Message { get; }

        public ErrorResponseException(HttpStatusCode statusCode, string message = "") : base(message)
        {
            StatusCode = statusCode;
            Message = message;
        }
    }
}
