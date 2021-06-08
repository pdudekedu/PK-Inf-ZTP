using System;
using System.Net;

namespace WorkManager.Infrastructure.ErrorHandling.Exceptions
{
    public class UnauthorizedException : HttpStatusCodeException
    {
        public UnauthorizedException(string message, Exception innerException = null) 
            : base(HttpStatusCode.Unauthorized, message, innerException)
        {

        }
    }
}
