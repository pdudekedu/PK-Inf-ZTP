using System;

namespace WorkManager.ErrorHandling
{
    internal static class ErrorHandlingExtensions
    {
        internal static ErrorDetails ToErrorDetails(this Exception ex)
        {
            return new ErrorDetails(System.Net.HttpStatusCode.InternalServerError, "Wystąpił błąd po stronie serwera.");
        }
    }
}
