using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace WorkManager.ErrorHandling.Exceptions
{
    public class NotFoundException : HttpStatusCodeException
    {
        public NotFoundException(string message, Exception innerException = null) 
            : base(HttpStatusCode.NotFound, message, innerException)
        {

        }
    }
}
