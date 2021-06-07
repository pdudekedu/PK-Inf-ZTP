using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace WorkManager.ErrorHandling.Exceptions
{
    public class UnauthorizedException : HttpStatusCodeException
    {
        public UnauthorizedException(string message, Exception innerException = null) 
            : base(HttpStatusCode.Unauthorized, message, innerException)
        {

        }
    }
}
