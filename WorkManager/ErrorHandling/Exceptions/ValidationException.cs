using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace WorkManager.ErrorHandling.Exceptions
{
    public class ValidationException : HttpStatusCodeException
    {
        public string[] Messages { get; }
        public ValidationException(params string[] messages) : base(HttpStatusCode.BadRequest)
        {
            Messages = messages;
        }

        public override ErrorDetails ToErrorDetails()
        {
            return new ErrorDetails(StatusCode, Messages);
        }
    }
}
