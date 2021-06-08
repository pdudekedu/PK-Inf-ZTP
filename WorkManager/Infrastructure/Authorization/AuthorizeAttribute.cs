using Microsoft.AspNetCore.Mvc.Filters;
using System;
using WorkManager.Persistence.Entities;
using WorkManager.Infrastructure.ErrorHandling.Exceptions;

namespace WorkManager.Infrastructure.Authorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = (User)context.HttpContext.Items["User"];

            if (user == null)
            {
                throw new UnauthorizedException("Użytkownik niezalogowany");
            }
        }
    }
}
