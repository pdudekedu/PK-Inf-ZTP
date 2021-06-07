using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace WorkManager.ErrorHandling.Exceptions
{
    public class ConflictException : HttpStatusCodeException
    {
        public ConflictException(string message, Exception innerException = null) 
            : base(HttpStatusCode.Conflict, message, innerException)
        {
        }
    }
}
