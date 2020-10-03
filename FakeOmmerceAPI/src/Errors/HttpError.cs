using System;
using System.Net;

namespace FakeOmmerce.Errors
{
    public class HttpError : Exception
    {
        public object HttpErrorResponse { get; }

        public HttpError(string errorMessage, HttpStatusCode statusCode) : base(errorMessage)
        {
            HttpErrorResponse = new {
                statusCode = statusCode,
                message = errorMessage
            };
        }
    }
}