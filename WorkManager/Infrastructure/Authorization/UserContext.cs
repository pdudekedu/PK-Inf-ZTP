using Microsoft.AspNetCore.Http;
using WorkManager.Infrastructure.ErrorHandling.Exceptions;
using WorkManager.Persistence.Entities;

namespace WorkManager.Infrastructure.Authorization
{
    public interface IUserContext
    {
        User User { get; }
    }

    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _accesor;

        public UserContext(IHttpContextAccessor accesor)
        {
            _accesor = accesor;
        }

        public User User
        {
            get
            {
                return (User)_accesor.HttpContext.Items[JwtConfig.UserItem]
                    ?? throw new UnauthorizedException("Użytkonik niezalogowany.");
            }
        }
    }
}
