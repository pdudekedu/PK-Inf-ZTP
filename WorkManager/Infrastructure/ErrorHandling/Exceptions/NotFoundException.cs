﻿using System;
using System.Net;

namespace WorkManager.Infrastructure.ErrorHandling.Exceptions
{
    public class NotFoundException : HttpStatusCodeException
    {
        public NotFoundException(string message, Exception innerException = null) 
            : base(HttpStatusCode.NotFound, message, innerException)
        {

        }
    }
}
