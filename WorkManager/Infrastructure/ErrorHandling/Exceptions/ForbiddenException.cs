using System;
using System.Net;

namespace WorkManager.Infrastructure.ErrorHandling.Exceptions
{
    public class ForbiddenException : HttpStatusCodeException
    {
        public ForbiddenException(string message, Exception innerException = null) 
            : base(HttpStatusCode.Forbidden, message, innerException)
        {

        }
    }
}
